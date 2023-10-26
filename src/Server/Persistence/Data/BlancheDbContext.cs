using Domain.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BlancheDbContext : DbContext
{
  public BlancheDbContext(DbContextOptions<BlancheDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Formula>(builder =>
    {
      builder.OwnsOne(f => f.Description);
    });

    modelBuilder.Entity<Equipment>(builder =>
    {
      builder.OwnsOne(e => e.Description);
    });
  }

  public DbSet<Formula> Formulas => Set<Formula>();
  public DbSet<Equipment> Equipments => Set<Equipment>();
}
