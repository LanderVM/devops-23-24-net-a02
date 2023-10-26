using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class FormulaConfiguration : EntityConfiguration<Formula>
{
  public override void Configure(EntityTypeBuilder<Formula> builder)
  {
    builder.OwnsOne<Description>(f => f.Description);
  }
}
