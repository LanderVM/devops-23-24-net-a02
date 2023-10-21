using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = "server=localhost;user=root;password=ewdjProject$$2;database=devopsA02";
// new MariaDbServerVersion
// new MySqlServerVersion(new Version(8, 0, 34));
var serverVersion = ServerVersion.AutoDetect(dbConnection);

builder.Services.AddDbContext<DomainContext>(
  dbContextOptions =>
    /*{
      if (builder.Environment.IsDevelopment())
        dbContextOptions.UseMySql(dbConnection, serverVersion)
          .LogTo(Console.WriteLine, LogLevel.Information)
          .EnableSensitiveDataLogging()
          .EnableDetailedErrors();
      else
        dbContextOptions.UseMySql(dbConnection, serverVersion);
    }*/
    dbContextOptions
      .UseMySql(dbConnection, serverVersion)
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging()
      .EnableDetailedErrors()
);

// TODO service for CRUD here

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// TODO authorization
// app.UseAuthorization();

// TODO rest api controllers

app.CreateDbIfNotExists();

app.MapGet("/", () => "Navigate to /swagger for Swagger test UI");

app.Run();
