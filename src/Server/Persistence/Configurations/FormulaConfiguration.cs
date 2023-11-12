using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Server.Persistence.Configurations;

public class FormulaConfiguration : EntityConfiguration<Formula>
{
  public override void Configure(EntityTypeBuilder<Formula> builder)
  {
    base.Configure(builder);
    builder.HasMany(f => f.Equipment)
      .WithMany(e => e.Formulas)
      .UsingEntity(join => join.ToTable("FormulaEquipment"));
    builder.OwnsOne(e => e.Description, a =>
    {
      a.WithOwner();
      a.Property(d => d.Attributes)
        .HasConversion(
          c => JsonConvert.SerializeObject(c),
          c => JsonConvert.DeserializeObject<List<string>>(c) ?? new List<string>())
        .IsRequired();
    });
    builder.Property(f => f.BasePrice)
      .HasConversion(
        c => JsonConvert.SerializeObject(c),
        c => JsonConvert.DeserializeObject<List<decimal>>(c) ?? new List<decimal>())
      .IsRequired();
    builder.Property(f => f.PricePerDayExtra)
      .IsRequired();
  }
}
