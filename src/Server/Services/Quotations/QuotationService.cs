using Domain.Customers;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
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

    var customer = _dbContext.Customers.FirstOrDefault(customerFromDb =>
      customerFromDb.FirstName == model.Customer.FirstName
      && customerFromDb.LastName == model.Customer.LastName
      && customerFromDb.Email.Value == model.Customer.Email.Email
      && customerFromDb.PhoneNumber.Value == model.Customer.PhoneNumber
      && customerFromDb.VatNumber == model.Customer.VatNumber);
    if (customer is null)
    {
      customer = new Customer(
        model.Customer.FirstName,
        model.Customer.LastName,
        GetCustomerEmail(model)
        ?? new Email(model.Customer.Email.Email),
        GetCustomerAddress(model)
        ?? new Address(model.Customer.BillingAddress.Street,
          model.Customer.BillingAddress.HouseNumber,
          model.Customer.BillingAddress.City,
          model.Customer.BillingAddress.PostalCode),
        new PhoneNumber(model.Customer.PhoneNumber),
        model.Customer.VatNumber);

      _dbContext.Customers.Add(customer);
    }

    Quotation quotation = new Quotation(
      chosenFormula,
      customer,
      GetEventLocation(model)
      ?? new Address(
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

  private Address? GetCustomerAddress(QuotationDto.Create model)
  {
    return _dbContext.Customers.Include(customer => customer.BillingAddress)
      .FirstOrDefault(customerFromDb =>
        customerFromDb.BillingAddress.Street == model.Customer.BillingAddress.Street
        && customerFromDb.BillingAddress.HouseNumber == model.Customer.BillingAddress.HouseNumber
        && customerFromDb.BillingAddress.PostalCode == model.Customer.BillingAddress.PostalCode
        && customerFromDb.BillingAddress.City == model.Customer.BillingAddress.City)?.BillingAddress;
  }

  private Address? GetEventLocation(QuotationDto.Create model)
  {
    return _dbContext.Quotations.Include(quotation => quotation.EventLocation)
      .FirstOrDefault(quotationFromDb =>
        quotationFromDb.EventLocation.Street == model.EventLocation.Street
        && quotationFromDb.EventLocation.HouseNumber == model.EventLocation.HouseNumber
        && quotationFromDb.EventLocation.PostalCode == model.EventLocation.PostalCode
        && quotationFromDb.EventLocation.City == model.EventLocation.City)?.EventLocation;
  }
}
