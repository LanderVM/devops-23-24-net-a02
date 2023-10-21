using Domain.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class DomainContext : DbContext
{
  public DomainContext(DbContextOptions<DomainContext> options) : base(options) { }

  public DbSet<Formula> Formulas => Set<Formula>();
  public DbSet<Equipment> Equipments => Set<Equipment>();
}
