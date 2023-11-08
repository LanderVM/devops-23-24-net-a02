using Domain.Formulas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class EquipmentConfiguration : EntityConfiguration<Equipment>
{
  public override void Configure(EntityTypeBuilder<Equipment> builder)
  {
    base.Configure(builder);
    builder.OwnsOne<Description>(f => f.Description);
  }
}
