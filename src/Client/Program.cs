using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using devops_23_24_net_a02.Client;
using MudBlazor.Services;
using devops_23_24_net_a02.Client.Pages.ExtraMaterial;
using shared.Equipment;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using devops_23_24_net_a02.Client.Shared;
using shared.Formulas;
using devops_23_24_net_a02.Client.Pages.Formulas;
using shared.Quotations;
using devops_23_24_net_a02.Client.Pages.Quotations;
using Append.Blazor.Sidepanel;
using devops_23_24_net_a02.Client.Files;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Attaches access token to each HTTP request
builder.Services.AddHttpClient("FoodtruckAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
       .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
       .CreateClient("FoodtruckAPI"));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<PersonalDetailsState>();
builder.Services.AddScoped<ExtraMaterialState>();
builder.Services.AddScoped<QuotationEstimateState>();
builder.Services.AddScoped<FormulaState>();
builder.Services.AddScoped<EventDetailsState>();

builder.Services.AddSidepanel();
builder.Services.AddHttpClient<IStorageService,
                               AzureBlobStorageService>();

builder.Services.AddMudServices(options =>
{
  options.PopoverOptions.ThrowOnDuplicateProvider = false;
});

builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IQuotationService, QuotationService>();

//Authentication
builder.Services.AddOidcAuthentication(options =>
{
  builder.Configuration.Bind("Auth0", options.ProviderOptions);
  options.ProviderOptions.ResponseType = "code";
  options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();
