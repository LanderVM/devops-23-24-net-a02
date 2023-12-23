using Api.Data;
using devops_23_24_net_a02.Middlewares;
using devops_23_24_net_a02.Shared.Emails;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Authorization
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.Authority = builder.Configuration["Auth0:Authority"];
  options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});

builder.Services.AddServices();

builder.Services.AddValidatorsFromAssemblyContaining<EmailDto.Create.Validator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(type =>
    type.DeclaringType is null ? $"{type.Name}" : $"{type.DeclaringType?.Name}.{type.Name}");
  options.EnableAnnotations();
}).AddFluentValidationRulesToSwagger();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BlancheDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
  app.UseSwagger();
  app.UseSwaggerUI();
  app.CreateDbIfNotExists();
}
else
{
  //app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

// Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
