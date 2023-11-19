using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Persistence.Triggers;

namespace Api.Data;

public class BlancheDbContext : DbContext
{
  private readonly IConfiguration _configuration;

  public BlancheDbContext(DbContextOptions<BlancheDbContext> options, IConfiguration configuration) : base(options)
  {
    _configuration = configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
  {
    
  var dbConnection = _configuration.GetConnectionString("DBConnectionString");
  var serverVersion = ServerVersion.AutoDetect(dbConnection);
    var mail = _configuration.GetSection("MailSettings").GetSection("MailAdress").Value;
    var password = _configuration.GetSection("MailSettings").GetSection("Password").Value;
    EmailConfiguration emailConfig = EmailConfiguration.CreateInstance(mail, password);

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
