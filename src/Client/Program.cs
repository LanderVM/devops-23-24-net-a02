using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using devops_23_24_net_a02.Client;
using MudBlazor.Services;
using devops_23_24_net_a02.Client.Pages.ExtraMaterial;
using shared.Equipment;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<PersonalDetailsState>();
builder.Services.AddMudServices();
builder.Services.AddMudServices(options =>
{
  options.PopoverOptions.ThrowOnDuplicateProvider = false;
});

builder.Services.AddMudServices();

builder.Services.AddScoped<IEquipmentService, EquipmentService>();

await builder.Build().RunAsync();
