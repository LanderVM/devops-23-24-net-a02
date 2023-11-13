using Domain.Formulas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Server.Persistence.Configurations;

public class EquipmentConfiguration : EntityConfiguration<Equipment>
{
  public override void Configure(EntityTypeBuilder<Equipment> builder)
  {
    base.Configure(builder);
    builder.OwnsOne(e => e.Description, a =>
    {
      a.WithOwner();
      a.Property(d => d.Attributes)
        .HasConversion(
          c => JsonConvert.SerializeObject(c),
          c => JsonConvert.DeserializeObject<List<string>>(c) ?? new List<string>())
        .IsRequired();
    });
    builder.Property(e => e.Price)
      .IsRequired();
    builder.Property(e => e.Stock)
      .IsRequired();
  }
}
