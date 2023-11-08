using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Persistence.Configurations;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.ToTable(typeof(T).Name); 
    builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).ValueGeneratedNever();
    builder.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
    builder.Property(x => x.UpdatedAt).HasDefaultValueSql("NOW()").IsConcurrencyToken();
  }
}
