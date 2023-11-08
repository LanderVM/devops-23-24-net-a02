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
    builder.HasOne<Email>(c => c.Email);
    builder.OwnsOne<PhoneNumber>(c => c.PhoneNumber).Property(p => p.Value);
    builder.HasOne<Address>(c => c.BillingAddress)
      .WithMany(a => a.BillingAddresses)
      .IsRequired();
  }
}
