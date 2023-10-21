using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO move sensitive data to environment
var dbConnection = "server=localhost;user=root;password=ewdjProject$$2;database=devopsA02";
var serverVersion = ServerVersion.AutoDetect(dbConnection);

builder.Services.AddDbContext<DomainContext>(
  dbContextOptions =>
  {
    if (builder.Environment.IsDevelopment())
      dbContextOptions.UseMySql(dbConnection, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
    else
      dbContextOptions.UseMySql(dbConnection, serverVersion);
  }
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
