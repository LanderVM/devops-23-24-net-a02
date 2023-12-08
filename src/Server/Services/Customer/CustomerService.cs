using Api.Data;
using Domain.Customers;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Shared.Customer;
using Shared.Customers;
using Domain.Exceptions;

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
    Customer? c = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Email.Value.Equals(model.Email.Email));

    if (c is not null) {
      throw new EntityAlreadyExistsException(nameof(Customer),nameof(Customer.Email),model.Email.Email);
    }

    Email email = new Email(model.Email.Email);
    BillingAddress billingAddress = new BillingAddress(model.BillingAddress.Street, model.BillingAddress.HouseNumber, model.BillingAddress.City, model.BillingAddress.PostalCode);
    PhoneNumber phone = new PhoneNumber(model.PhoneNumber);

    Customer customer = new Customer(model.FirstName, model.LastName, email, billingAddress, phone, model.VatNumber);
    _dbContext.Customers.Add(customer);
    await _dbContext.SaveChangesAsync();
    return customer.Id;
  }

  public async Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request)
  {
    var query = _dbContext.Customers.AsQueryable();

    int totalAmount = await query.CountAsync();

    var items = await query
       .Skip((request.Page - 1) * request.PageSize)
       .Take(request.PageSize)
       .OrderBy(x => x.Id)
       .Select(x => new CustomerDto.Details
       {
         Id = x.Id,
         FirstName = x.FirstName,
         LastName = x.LastName,
       }).ToListAsync();

    var result = new CustomerResult.Index
    {
      Customers = items,
      TotalAmount = totalAmount
    };
    return result;
  }
}
