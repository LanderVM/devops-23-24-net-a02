using Domain.Formulas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class EquipmentConfiguration : EntityConfiguration<Equipment>
{
  public override void Configure(EntityTypeBuilder<Equipment> builder)
  {
    base.Configure(builder);
    builder.OwnsOne(e => e.Description)
      .WithOwner();
    builder.Property(e => e.Price)
      .HasPrecision(2)
      .IsRequired();
    builder.Property(e => e.Stock)
      .IsRequired();
  }
}
