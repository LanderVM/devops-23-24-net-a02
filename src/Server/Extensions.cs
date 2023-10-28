using Microsoft.EntityFrameworkCore;
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
    return services;
  }
}
