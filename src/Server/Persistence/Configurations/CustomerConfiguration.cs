using System.Net.Mail;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class CustomerConfiguration : EntityConfiguration<Customer>
{
  public override void Configure(EntityTypeBuilder<Customer> builder)
  {
    base.Configure(builder);
    builder.Property(c => c.FirstName)
      .IsRequired();
    builder.Property(c => c.LastName)
      .IsRequired();
    builder.OwnsOne(c => c.BillingAddress, address =>
    {
      address.Property(a => a.Street).IsRequired();
      address.Property(a => a.HouseNumber).IsRequired();
      address.Property(a => a.City).IsRequired();
      address.Property(a => a.PostalCode).IsRequired();
    });
    builder.OwnsOne(c => c.PhoneNumber)
      .Property(p => p.Value)
      .IsRequired();
  }
}
