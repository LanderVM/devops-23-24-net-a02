namespace Server.Services;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IEmailService, EmailService>();
    return services;
  }
}
