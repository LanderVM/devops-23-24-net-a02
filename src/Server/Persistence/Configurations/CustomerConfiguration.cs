using System.Net.Mail;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class CustomerConfiguration : EntityConfiguration<Customer>
{
  public override void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.OwnsOne<Email>(c => c.Email).Property(e => e.Value)
      .HasConversion(e => e.ToString(), e => new MailAddress(e)) /*.HasColumnName()*/;
    builder.OwnsOne<PhoneNumber>(c => c.PhoneNumber).Property(p => p.Value);
    builder.OwnsOne<Address>(c => c.Address, address =>
    {
      address.Property(a => a.Street);
      address.Property(a => a.HouseNumber);
      address.Property(a => a.City);
      address.Property(a => a.PostalCode);
    });
  }
}
