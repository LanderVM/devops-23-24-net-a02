using Domain.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BlancheDbContext : DbContext
{
  private readonly IConfiguration Configuration;

  public BlancheDbContext(DbContextOptions<BlancheDbContext> options, IConfiguration configuration) : base(options)
  {
    Configuration = configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
  {
    
  var dbConnection = Configuration.GetConnectionString("DBConnectionString");
  var serverVersion = ServerVersion.AutoDetect(dbConnection);
  
    dbContextOptionsBuilder.UseMySql(dbConnection, serverVersion)
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging()
      .EnableDetailedErrors();
  }

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
