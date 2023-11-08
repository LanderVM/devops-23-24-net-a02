using Microsoft.EntityFrameworkCore;
using server.Services;
using shared.Equipment;
using Server.Services;

namespace Api.Data;

public static class Extensions
{
  public static void CreateDbIfNotExists(this IHost host)
  {
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BlancheDbContext>();
        // context.Database.EnsureCreated();
        context.Database.Migrate();
        DbInitializer.Initialize(context);
      }
    }
  }
  
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IEquipmentService,EquipmentService>();
    return services;
  }
}
