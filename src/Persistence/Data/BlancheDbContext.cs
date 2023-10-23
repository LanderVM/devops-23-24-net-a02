using Domain.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BlancheDbContext : DbContext
{
  public BlancheDbContext(DbContextOptions<BlancheDbContext> options) : base(options) { }

  public DbSet<Formula> Formulas => Set<Formula>();
  public DbSet<Equipment> Equipments => Set<Equipment>();
}
