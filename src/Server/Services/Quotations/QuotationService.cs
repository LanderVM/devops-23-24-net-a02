using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using shared.Equipment;
using shared.Formulas;
using Shared.Common;
using Shared.Quotations;

namespace Api.Data.Services.Quotations;

public class QuotationService : IQuotationService
{
  private readonly BlancheDbContext _dbContext;

  public QuotationService(BlancheDbContext dbContext)
  {
    _dbContext = dbContext;
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
      new List<QuotationLine>(),
      model.StartTime,
      model.EndTime);

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

  /*public async Task<decimal> GetEstimatedQuotationPrice()
  {
    var formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == model.FormulaId);

    Formula formula2 = new Formula(formula.Equipment, formula.Description.Title, formula.Description.Attributes);
    Quotation quotation = new Quotation(formula, model.StartTime, model.EndTime, model.EstimatedNumberPeople, model.IsTripelBier);
    return quotation.GetEstimatedPrice();
    
  }*/

  public async Task<QuotationDto.Details> GetPriceEstimationDetails()
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

    var result = new QuotationDto.Details
    {
      Formulas = itemsFormulas,
      Equipment = itemsEquipment,
      UnavailableDates = unavailableDates,
    };

    return result;
  }
}
