using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Server.Persistence.Triggers;

namespace Api.Data;

public class BlancheDbContext : DbContext
{
  private readonly IConfiguration _configuration;

  public BlancheDbContext(DbContextOptions<BlancheDbContext> options, IConfiguration configuration) : base(options)
  {
    _configuration = configuration;
  }

  // TODO
  /*protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    // All decimals should have 2 digits after the comma
    configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    // Max Length of a NVARCHAR that can be indexed
    configurationBuilder.Properties<string>().HaveMaxLength(4_000);
  }*/
  
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
  {
    
  var dbConnection = _configuration.GetConnectionString("DBConnectionString");
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
  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Email> Emails => Set<Email>();
  public DbSet<Quotation> Quotations => Set<Quotation>();
}
