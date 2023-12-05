using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public static class DbInitializer
{
  public static void Initialize(BlancheDbContext context)
  {

    if (context.Formulas.Any()
        && context.Equipments.Any()
        && context.Customers.Any()
        && context.Quotations.Any())
    {
      return; // DB seeded
    }


    // Extra Items 
    var equipment = new[]
    {
      new Equipment("Saladette", new List<string> { "34cm x 120cm x 28 cm", "Inclusief 5 st GN 1/4 bakken met deksels" }, 65M, 2, "https://a2blanchestorage.blob.core.windows.net/images/Saladette.jpg"),
      new Equipment("GN bak 1/4", new List<string> { "diepte 150mm", "Inclusief deksel" }, 3M, 20, "https://a2blanchestorage.blob.core.windows.net/images/GN bakken 1-4.jpg"),
      new Equipment("GN bak 1/6", new List<string> { "diepte 150mm", "Inclusief deksel" }, 3M, 10, "https://a2blanchestorage.blob.core.windows.net/images/GN bakken 1-6.jpg"), 
      new Equipment("Barkoeler 320l", new List<string> { "50cm x 135cm x 87cm", "3x glazen schuifdeuren", "Kleur zwart" }, 65M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Barkoeler 320 liter.jpg"),
      new Equipment("Cocktail glas gouden rand", new List<string> { "330ml" }, 0.50M, 100, "https://a2blanchestorage.blob.core.windows.net/images/Cocktail glas gouden rand.jpg"),
      new Equipment("Cocktail glas gemiddeld", new List<string> { "330ml" }, 0.20M, 100, "https://a2blanchestorage.blob.core.windows.net/images/Cocktail glas type rand.jpg"),
      new Equipment("Cocktail glas klein", new List<string> { "250ml" }, 0.15M, 100, "https://a2blanchestorage.blob.core.windows.net/images/Cocktailglazen.jpg"),
      new Equipment("Lichtslinger", new List<string> { "guirlandes 10m" }, 15M, 4, "https://a2blanchestorage.blob.core.windows.net/images/Lichtslinger.jpg"),
      new Equipment("Ijsemmer", new List<string> { "7l" }, 10M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Ijsemmer.jpg"),
      new Equipment("Vuurschaal", new List<string> { "diameter 120cm" }, 40M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Vuurschaal.jpg"),
      new Equipment("Driepoot met BBQ rooster", new List<string> { "Inclusief vuurschaal" }, 100M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Driepoot met BBQ.jpg"),
      new Equipment("Diepvries 80l", new List<string> { "60cm x 60cm x 80cm" }, 50M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Diepvries.jpg"),
      new Equipment("Dienblad", new List<string> { "diameter 35cm", "Anti-slip", "Kleur zwart" }, 1.5M, 10, "https://a2blanchestorage.blob.core.windows.net/images/Plateaus.jpg"),
      new Equipment("Snijplank", new List<string> { "60cm x 40cm", "Kleur groen" }, 4M, 3, "https://a2blanchestorage.blob.core.windows.net/images/Snijplank.jpg"),
      new Equipment("Spoelbak klein", new List<string> { "100cm x 50cm x 80cm", "Type camping" }, 10M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Spoelbak.jpg"),
      new Equipment("Drankendispenser", new List<string>(), 10M, 1, "https://a2blanchestorage.blob.core.windows.net/images/Drankendispenser.jpg"),
      new Equipment("Soepketel", new List<string> { "10l", "Inclusief pollepel" }, 15M, 2, "https://a2blanchestorage.blob.core.windows.net/images/Soepketel.png"),
      new Equipment("Strobaal", new List<string> { "80cm x 45cm x 45cm" }, 4M, 10, "https://a2blanchestorage.blob.core.windows.net/images/Strobalen.jpg"),
      new Equipment("Schapenvacht", new List<string> { "ongev. 100cm x 50cm" }, 12M, 10, "https://a2blanchestorage.blob.core.windows.net/images/Schapenvacht.jpg"),
      new Equipment("Biertafelset", new List<string> { "220cm x 130cm x 48cm", "1 tafel met 2 banken" }, 15M, 5, "https://a2blanchestorage.blob.core.windows.net/images/Biertafel.png"),
      new Equipment("Fruitkist", new List<string> { "50cm x 41cm x 31cm" }, 5M, 20, "https://a2blanchestorage.blob.core.windows.net/images/Fruitkisten.jpg")
    };
    
    // Items included in Formula's
    var vatenBier = new Equipment("Vaten bier", new List<string> { "Inbegrepen in formule" }, 1.5M, 999, "https://a2blanchestorage.blob.core.windows.net/images/bier-vat.jpg");
    var glazen = new Equipment("Glazen", new List<string> { "Inbegrepen in formule" }, 1.5M, 999, "https://a2blanchestorage.blob.core.windows.net/images/Cocktailglazen.jpg");
    var bbq = new Equipment("Bbq met bbq-kit", new List<string> { "Inbegrepen in formule", "Inclusief hout voor bbq" },
      12M, 999 , "https://a2blanchestorage.blob.core.windows.net/images/BBQ.jpg");

    // Formula's
    var formulas = new Formula[]
    {
      new(new List<Equipment>(), "Basic", new List<string> { "De foodtruck" }),
      new(new List<Equipment>(), "Extended", new List<string> { "De foodtruck", "Met bier (keuze uit pils of tripel)", "Glazen inbegrepen" }),
      new(new List<Equipment>(), "All-in",
        new List<string> { "De foodtruck", "Met bier (keuze uit pils of tripel)", "Glazen inbegrepen", "Inclusief barbecue", "Inclusief hout & eten voor de bbq", "Bbq-kit ingebegrepen" })
    };

    // Customers
    var cust1 = new Customer("Bert", "de Backer", new Email("bert.debacker@gmail.com"),
      new BillingAddress("Rue de Bouillon", "52", "Grimbergen", "1850"), new PhoneNumber("0486980477"), "BE123");
    var cust2 = new Customer("Frederick", "Honderdpoot", new Email("f.honderdpoot@outlook.be"),
      new BillingAddress("Korte Noordsstraat", "292", "Uitkerke", "8370"), new PhoneNumber("0479894230"), null);
    var customers = new[] { cust1, cust2 };

    var eventLoc1 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc2 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc3 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc4 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc5 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc6 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc7 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc8 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc9 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc10 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");
    var eventLoc11 = new EventLocation("Rue de Bouillon", "52", "Grimbergen", "1850");


    // Quotations
    var quote1 = new Quotation(formulas[0], customers[0], eventLoc1, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote2 = new Quotation(formulas[2], customers[1], eventLoc2,
      new List<QuotationLine> { new(equipment[5], 30), new(equipment[8], 1), },
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote3 = new Quotation(formulas[0], customers[0], eventLoc3, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote4 = new Quotation(formulas[0], customers[0], eventLoc4, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote5 = new Quotation(formulas[2], customers[1], eventLoc5,
      new List<QuotationLine> { new(equipment[5], 30), new(equipment[8], 1), },
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote6 = new Quotation(formulas[0], customers[0], eventLoc6, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote7 = new Quotation(formulas[0], customers[0], eventLoc7, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote8 = new Quotation(formulas[2], customers[1], eventLoc8,
      new List<QuotationLine> { new(equipment[5], 30), new(equipment[8], 1), },
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote9 = new Quotation(formulas[0], customers[0], eventLoc9, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote10 = new Quotation(formulas[2], customers[1], eventLoc10,
      new List<QuotationLine> { new(equipment[5], 30), new(equipment[8], 1), },
      DateTime.Today, DateTime.Today.AddDays(2));
    var quote11 = new Quotation(formulas[0], customers[0], eventLoc11, new List<QuotationLine>(),
      DateTime.Today, DateTime.Today.AddDays(2));
    
    var quotes = new[] { quote1, quote2, quote3, quote4, quote5, quote6, quote7, quote8, quote9, quote10, quote11};

    // Save to db
    context.Equipments.AddRange(equipment);
    context.Formulas.AddRange(formulas);
    context.Customers.AddRange(customers);
    context.Quotations.AddRange(quotes);
    context.SaveChanges();
  }
}
