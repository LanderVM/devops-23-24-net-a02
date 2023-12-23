﻿using Api.Data.Services.Quotations;
using devops_23_24_net_a02.Services.Files;
using devops_23_24_net_a02.Shared.Emails;
using Microsoft.EntityFrameworkCore;
using server.Services;
using Server.Services;
using shared.Equipment;
using shared.Formulas;
using shared.GoogleMaps;
using shared.Quotations;

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
        context.Database.Migrate();
        DbInitializer.Initialize(context);
      }
    }
  }

  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IEquipmentService, EquipmentService>();
    services.AddScoped<IQuotationService, QuotationService>();
    services.AddScoped<IFormulaService, FormulaService>();
    services.AddScoped<IStorageService, BlobStorageService>();
    services.AddScoped<IGoogleMapsService, GoogleMapsService>();

    return services;
  }
}
