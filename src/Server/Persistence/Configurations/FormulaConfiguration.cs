using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class FormulaConfiguration : EntityConfiguration<Formula>
{
  public override void Configure(EntityTypeBuilder<Formula> builder)
  {
    base.Configure(builder);
    builder.HasMany(f => f.Equipment)
      .WithMany(e => e.Formulas)
      .UsingEntity(join => join.ToTable("FormulaEquipment"));
    builder.OwnsOne(f => f.Description)
      .WithOwner();
    builder.Property(f => f.PricePerDay)
      .HasPrecision(2)
      .IsRequired();
  }
}
