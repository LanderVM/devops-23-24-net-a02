﻿using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public static class DbInitializer
{
  public static void Initialize(BlancheDbContext context)
  {
    context.Database.Migrate();
    
    if (context.Formulas.Any()
        && context.Equipments.Any()
        && context.Customers.Any()
        && context.Quotations.Any())
    {
      return; // DB seeded
    }

    var bbq = new Equipment( "Barbecue", "MMmmm Tasty", 20M, 50);
    var tent = new Equipment( "Tent", "To keep the party clean", 30M, 5);
    var barrel = new Equipment("Barrel", "Storing the goods", 10M, 20);

    var equipment = new Equipment[] {
      new Equipment(img, "Bucket1","Damm!",15M,20),
    new Equipment(img, "Bucket2", "Damm!", 17M, 30),
    new Equipment(img, "Bucket3", "Damm!", 12M, 25),
    new Equipment(img, "Bucket4", "Damm!", 44M, 10),
    new Equipment(img, "Bucket5", "Damm!", 11M, 15),
    new Equipment(img, "Bucket6", "Damm!", 54M, 7),
    new Equipment(img, "Bucket7", "Damm!", 19M, 9)
  };

    var formulas = new Formula[]
    {
      new(new List<Equipment>(), "Basic", "For a small party", 20M),
      new(new List<Equipment> { bbq, tent }, "Extended", "For a medium sized party", 30M),
      new(new List<Equipment> { bbq, tent, barrel }, "All-in", "For the best party you could imagine", 40M)
    };

    var cust1 = new Customer("Bert", "de Backer", new Email("bert.debacker@gmail.com"),
      new Address("Rue de Bouillon", "52", "Grimbergen", "1850"), new PhoneNumber("0486980477"));
    var cust2 = new Customer("Frederick", "Honderdpoot", new Email("f.honderdpoot@outlook.be"),
      new Address("Korte Noordsstraat", "292", "Uitkerke", "8370"), new PhoneNumber("0479894230"));
    var customers = new[] { cust1, cust2 };

    var quote1 = new Quotation(formulas[0], customers[0], customers[0].BillingAddress, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote2 = new Quotation(formulas[2], customers[1], customers[1].BillingAddress,
      new List<QuotationLine> { new(barrel, 3), new(tent, 1), },
      DateTime.Today, DateTime.Today.AddDays(2));
    var quotes = new[] { quote1, quote2 };

    context.Equipments.AddRange(equipment);
    context.Formulas.AddRange(formulas);
    context.Customers.AddRange(customers);
    context.Quotations.AddRange(quotes);
    context.SaveChanges();
  }
}
