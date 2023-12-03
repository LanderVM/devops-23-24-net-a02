﻿using Api.Data.Services.Quotations;
using Microsoft.EntityFrameworkCore;
using server.Services;
using shared.Equipment;
using Server.Services;
using shared.Quotations;
using shared.Formulas;

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
    services.AddScoped<ICustomerService, CustomerService>();
    return services;
  }
}
