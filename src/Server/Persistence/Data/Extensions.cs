using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
}
