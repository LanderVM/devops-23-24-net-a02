using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
  public void Configure(EntityTypeBuilder<Equipment> builder)
  {
    builder.OwnsOne<Description>(f => f.Description);
  }
}
