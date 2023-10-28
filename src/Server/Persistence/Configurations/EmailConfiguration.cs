using System.Net.Mail;
using Domain.Customers;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class EmailConfiguration : EntityConfiguration<Email>
{
  public override void Configure(EntityTypeBuilder<Email> builder)
  {
    builder.Property(e => e.Value)
      .HasConversion(e => e.ToString(), e => new MailAddress(e)) /*.HasColumnName()*/;
  }
}
