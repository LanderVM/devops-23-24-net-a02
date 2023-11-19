using Domain.Quotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Server.Persistence.Configurations;

public class QuotationConfiguration : EntityConfiguration<Quotation>
{
  public override void Configure(EntityTypeBuilder<Quotation> builder)
  {
    base.Configure(builder);
    builder.HasOne(q => q.Formula)
      .WithMany(f => f.OrderedIn)
      .IsRequired();
    builder.Property(q => q.OriginalFormulaPricePerDay)
      .HasConversion(
        c => JsonConvert.SerializeObject(c),
        c => JsonConvert.DeserializeObject<List<decimal>>(c) ?? new List<decimal>())
      .IsRequired();
    builder.Property(q => q.OriginalFormulaPricePerDayExtra)
      .IsRequired();
    builder.HasOne(q => q.OrderedBy)
      .WithMany(c => c.Quotations)
      .IsRequired();
    builder.OwnsOne(c => c.EventLocation, address =>
    {
      address.Property(a => a.Street).IsRequired();
      address.Property(a => a.HouseNumber).IsRequired();
      address.Property(a => a.City).IsRequired();
      address.Property(a => a.PostalCode).IsRequired();
    });
    builder.HasMany(q => q.QuotationLines)
      .WithOne(ql => ql.Quotation)
      .IsRequired();
    builder.Property(q => q.Status)
      .IsRequired();
    builder.Property(q => q.StartTime)
      .IsRequired();
    builder.Property(q => q.EndTime)
      .IsRequired();
    builder.Property(q => q.NumberOfPeople)
      .IsRequired();
  }
}
