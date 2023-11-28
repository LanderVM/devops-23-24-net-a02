using Domain.Common;
using Domain.Customers;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using shared.Quotations;

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

  public async Task<List<DateTime>> GetDatesAsync()
  {
    var query = _dbContext.Quotations.AsQueryable();

    IEnumerable <Quotation> quotations = await query.Where(x=>x.Status == QuotationStatus.Read).ToListAsync();

    List<DateTime> dateTimes = new List<DateTime>();

    foreach (var item in quotations)
    {
      DateTime startDate = item.StartTime;
      DateTime endDate = item.EndTime;

      while (startDate <= endDate)
      {
        dateTimes.Add(startDate);
        startDate = startDate.AddDays(1);
      }
    }

    return dateTimes;
  }
}
