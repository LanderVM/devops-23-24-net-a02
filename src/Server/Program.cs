using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllersWithViews(); TODO check for difference
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

// TODO move sensitive data to environment
var dbConnection = "server=172.168.1.10;user=vagrant;password='ewdjProject$$2';database=devopsA02";
var serverVersion = ServerVersion.AutoDetect(dbConnection);

builder.Services.AddDbContext<BlancheDbContext>(
  dbContextOptions =>
  {
    if (builder.Environment.IsDevelopment())
    {
      dbContextOptions.UseMySql(dbConnection, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
    }
    else
    {
      dbContextOptions.UseMySql(dbConnection, serverVersion);
    }
  }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
  app.UseSwagger();
  app.UseSwaggerUI();
  app.CreateDbIfNotExists();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
