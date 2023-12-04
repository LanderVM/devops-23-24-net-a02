using System.Linq;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shared.Equipment;
using shared.Formulas;
using Shared.Common;
using Shared.Customer;
using shared.Quotations;


namespace Api.Data.Services.Quotations;

public class QuotationService : IQuotationService
{
  private readonly BlancheDbContext _dbContext;

  public QuotationService(BlancheDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  
  public async Task<QuotationResult.Dates> GetDatesAsync()
  {
    var query = _dbContext.Quotations
      .Select(q => new QuotationDto.Dates { StartTime = q.StartTime, EndTime = q.EndTime });

    var dates = await query.ToListAsync();  

    var result = new QuotationResult.Dates { DateRanges = dates };
    return result;
    
  }

  public async Task<QuotationResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Quotations
      .Include(q => q.OrderedBy)          
      .ThenInclude(c => c.Email)      
      .OrderBy(q => q.Id)
      .Select(q => new QuotationDto.Index
      {
        QuotationId = q.Id,
        Customer = new CustomerDto.Index
        {
          FirstName = q.OrderedBy.FirstName,
          LastName = q.OrderedBy.LastName,
          Email = q.OrderedBy.Email.Value,
          
        },
        CreatedAt = q.CreatedAt.ToShortDateString()
      });

    var items = await query.ToListAsync();

    var result = new QuotationResult.Index
    {
      Quotation = items,
    };
    return result;

    
  }
  public async Task<int> CreateAsync(QuotationDto.Create model)
  {
    var chosenFormula = _dbContext.Formulas.FirstOrDefault(formula => formula.Id == model.FormulaId);
    if (chosenFormula is null)
      throw new ArgumentException($"Formula with id {model.FormulaId} does not exist!");
    if (chosenFormula.IsActive is false)
      throw new ArgumentException($"Formula with id {chosenFormula.Id} is not active!");
    
    var customer = _dbContext.Customers
      .Include(customer => customer.Email)
      .AsEnumerable()
      .FirstOrDefault(customerFromDb => EqualsCustomer(model, customerFromDb));
    if (customer is null)
    {
      // TODO als customer enkel nieuw adres of telefoonnummer heeft, oude op inactief zetten & nieuwe maken, of gewoon updaten?
      customer = new Customer(
        model.Customer.FirstName,
        model.Customer.LastName,
        GetCustomerEmail(model)
        ?? new Email(model.Customer.Email.Email),
        GetCustomerAddress(model)
        ?? new BillingAddress(model.Customer.BillingAddress.Street,
          model.Customer.BillingAddress.HouseNumber,
          model.Customer.BillingAddress.City,
          model.Customer.BillingAddress.PostalCode),
        new PhoneNumber(model.Customer.PhoneNumber),
        model.Customer.VatNumber);
      _dbContext.Customers.Add(customer);
    }
    
    
    var quotationLines = new List<QuotationLine>();
    foreach (var lines in model.Equipments)
    {
      
        var equipment = _dbContext.Equipments.FirstOrDefault(equipment => equipment.Id == lines.EquipmentId);
        quotationLines.Add(new QuotationLine(equipment, lines.Amount));
     
      
      
    }
    if (customer.Email.IsActive is false)
      customer.Email.IsActive = true; // TODO nieuwe maken of gewoon terug actief zetten?

    Quotation quotation = new Quotation(
      chosenFormula,
      customer,
      new EventLocation(
        model.EventLocation.Street,
        model.EventLocation.HouseNumber,
        model.EventLocation.City,
        model.EventLocation.PostalCode
      ),
      quotationLines,
      model.StartTime,
      model.EndTime,
      model.IsTripelBier
     );

    _dbContext.Quotations.Add(quotation);
    await _dbContext.SaveChangesAsync();
    return quotation.Id;
  }

 

  private Email? GetCustomerEmail(QuotationDto.Create model)
  {
    return _dbContext.Emails.FirstOrDefault(email => email.Value == model.Customer.Email.Email);
  }

  private BillingAddress? GetCustomerAddress(QuotationDto.Create model)
  {
    return _dbContext.Customers.Include(customer => customer.BillingAddress)
      .FirstOrDefault(customerFromDb =>
        customerFromDb.BillingAddress.Street == model.Customer.BillingAddress.Street
        && customerFromDb.BillingAddress.HouseNumber == model.Customer.BillingAddress.HouseNumber
        && customerFromDb.BillingAddress.PostalCode == model.Customer.BillingAddress.PostalCode
        && customerFromDb.BillingAddress.City == model.Customer.BillingAddress.City)?.BillingAddress;
  }

  private static bool EqualsCustomer(QuotationDto.Create model, Customer customerFromDb)
  {
    return customerFromDb.FirstName == model.Customer.FirstName
           && customerFromDb.LastName == model.Customer.LastName
           && customerFromDb.Email.Value == model.Customer.Email.Email
           && customerFromDb.PhoneNumber.Value == model.Customer.PhoneNumber
           && customerFromDb.VatNumber == model.Customer.VatNumber
           && customerFromDb.BillingAddress.Street == model.Customer.BillingAddress.Street
           && customerFromDb.BillingAddress.HouseNumber == model.Customer.BillingAddress.HouseNumber
           && customerFromDb.BillingAddress.PostalCode == model.Customer.BillingAddress.PostalCode
           && customerFromDb.BillingAddress.City == model.Customer.BillingAddress.City;
  }

  public async Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync()
  {
    var queryFormulas = _dbContext.Formulas.AsQueryable();

    IEnumerable<FormulaDto.Select> itemsFormulas = await queryFormulas.OrderBy(x => x.Id).Select(
      x => new FormulaDto.Select
      {
        Id = x.Id,
        Title = x.Description.Title,
      }
      ).ToListAsync();


    var queryEquipment = _dbContext.Equipments.AsQueryable();

    IEnumerable<EquipmentDto.Select> itemsEquipment = await queryEquipment.OrderBy(x => x.Id).Select(
      x => new EquipmentDto.Select
      {
        Id = x.Id,
        Title = x.Description.Title,
      }
      ).ToListAsync();


    var queryDates = _dbContext.Quotations.AsQueryable();

    IEnumerable<DateDto> unavailableDates = await queryDates.OrderBy(x => x.StartTime).Select(
      x => new DateDto
      {
        StartTime = x.StartTime.Ticks,
        EndTime = x.EndTime.Ticks
      }
      ).ToListAsync();

    var result = new QuotationResult.Detail
    {
      Formulas = itemsFormulas,
      Equipment = itemsEquipment,
      UnavailableDates = unavailableDates,
    };

    return result;
  }

  public async Task<decimal> GetPriceEstimationPrice(QuotationResponse.Estimate model)
  {
    decimal totalPrice = 0;

    var chosenFormula = _dbContext.Formulas.FirstOrDefault(formula => formula.Id == model.FormulaId);
    if (chosenFormula is null)
      throw new ArgumentException($"Formula with id {model.FormulaId} does not exist!");
    if (chosenFormula.IsActive is false)
      throw new ArgumentException($"Formula with id {chosenFormula.Id} is not active!"); var queryEquipment = _dbContext.Equipments.AsQueryable();

    List<EquipmentDto.Index>? equipmentDtoQuery = new List<EquipmentDto.Index>();

    if (model.EquipmentIds != null && model.EquipmentIds.Any())
    {
      equipmentDtoQuery = await queryEquipment
          .Where(x => model.EquipmentIds.Contains(x.Id))
          .Select(z => new EquipmentDto.Index
          {
            Id = z.Id,
            Price = z.Price,
          }).ToListAsync();
    }

    List<Equipment> equipmentList = new();
    foreach (var item in equipmentDtoQuery)
    {
      Equipment equipment = new(item.Price);
      equipmentList.Add(equipment);
    }

    Quotation quotation = new Quotation(new Formula(equipmentList), chosenFormula.Id, new DateTime(model.StartTime), new DateTime(model.EndTime), model.EstimatedNumberOfPeople, model.IsTripelBier);
    return quotation.GetEstimatedPrice();
  }
  
}
