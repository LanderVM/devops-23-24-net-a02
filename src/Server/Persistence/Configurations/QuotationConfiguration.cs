﻿using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class QuotationConfiguration : EntityConfiguration<Quotation>
{
  public override void Configure(EntityTypeBuilder<Quotation> builder)
  {
    base.Configure(builder);
    builder.HasOne<Formula>(q => q.Formula)
      .WithMany(f => f.OrderedIn)
      .IsRequired();
    builder.Property(q => q.OriginalFormulaPricePerDay)
      .HasPrecision(2)
      .IsRequired();
    builder.HasOne<Customer>(q => q.OrderedBy)
      .WithMany(c => c.Quotations)
      .IsRequired();
    builder.HasOne<Address>(q => q.EventLocation)
      .WithMany(a => a.EventLocations)
      .IsRequired();
    builder.HasMany<QuotationLine>(q => q.QuotationLines)
      .WithOne(ql => ql.Quotation)
      .IsRequired();
    builder.Property(q => q.Status)
      .IsRequired();
    builder.Property(q => q.StartTime)
      .IsRequired();
    builder.Property(q => q.EndTime)
      .IsRequired();
  }
}
