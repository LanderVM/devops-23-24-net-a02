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
    builder.HasOne(c => c.BillingAddress)
      .WithMany(a => a.BillingAddresses)
      .IsRequired();
    builder.OwnsOne(c => c.PhoneNumber)
      .Property(p => p.Value)
      .IsRequired();
  }
}
