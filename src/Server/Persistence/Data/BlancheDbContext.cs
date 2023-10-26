using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Server.Persistence.Triggers;

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
    
    dbContextOptionsBuilder.UseTriggers(options =>
    {
      options.AddTrigger<EntityBeforeSaveTrigger>();
    });
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlancheDbContext).Assembly);
  }

  public DbSet<Formula> Formulas => Set<Formula>();
  public DbSet<Equipment> Equipments => Set<Equipment>();
}
