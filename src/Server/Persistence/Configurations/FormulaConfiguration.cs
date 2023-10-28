using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class FormulaConfiguration : EntityConfiguration<Formula>
{
  public override void Configure(EntityTypeBuilder<Formula> builder)
  {
    builder.OwnsOne<Description>(formula => formula.Description);
    builder.HasMany(formula => formula.Equipment).WithMany(equipment => equipment.Formulas)
      .UsingEntity(join => join.ToTable("FormulaEquipment"));
  }
}
