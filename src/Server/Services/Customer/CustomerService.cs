using Api.Data;
using Common;
using Domain.Customers;
using Shared.Common;

namespace Server.Services;

public class CustomerService : ICustomerService
{
  private readonly BlancheDbContext _dbContext;

  public CustomerService(BlancheDbContext blancheDbContext)
  {
    _dbContext = blancheDbContext;
  }
  public async Task<int> CreateAsync(CustomerDto.Create model)
  {
    Email email = new Email(model.Email.Email);
    BillingAddress billingAddress = new BillingAddress(model.BillingAddress.Street, model.BillingAddress.HouseNumber, model.BillingAddress.City, model.BillingAddress.PostalCode);
    PhoneNumber phone = new PhoneNumber(model.PhoneNumber);

    Customer customer = new Customer(model.FirstName, model.LastName, email, billingAddress, phone, model.VatNumber);
    _dbContext.Customers.Add(customer);
    await _dbContext.SaveChangesAsync();
    return customer.Id;
  }
}
