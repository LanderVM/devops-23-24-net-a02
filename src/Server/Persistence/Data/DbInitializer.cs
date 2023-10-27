using Domain.Common;
using Domain.Customers;
using Domain.Formulas;

namespace Api.Data;

public static class DbInitializer
{
  public static void Initialize(BlancheDbContext context)
  {
    if (context.Formulas.Any()
        && context.Equipments.Any()
        && context.Customers.Any())
    {
      return; // DB seeded
    }

    var img = new Image("placeholder img", "placeholder text");

    var bbq = new Equipment(img, "Barbecue", "MMmmm Tasty", 20M, 50);
    var tent = new Equipment(img, "Tent", "To keep the party clean", 30M, 5);
    var barrel = new Equipment(img, "Barrel", "Storing the goods", 10M, 20);

    var formulas = new Formula[]
    {
      new(new List<Equipment>(), "Basic", "For a small party"),
      new(new List<Equipment> { bbq, tent }, "Extended", "For a medium sized party"),
      new(new List<Equipment> { bbq, tent, barrel }, "All-in", "For the best party you could imagine")
    };

    var cust1 = new Customer("Bert", "de Backer", new Email("bert.debacker@gmail.com"),
      new Address("Rue de Bouillon", "52", "Grimbergen", "1850"), new PhoneNumber("0486980477"));
    var cust2 = new Customer("Frederick", "Honderdpoot", new Email("f.honderdpoot@outlook.be"),
      new Address("Korte Noordsstraat", "292", "Uitkerke", "8370"), new PhoneNumber("0479894230"));

    var Customers = new Customer[] { cust1, cust2 };

    context.Formulas.AddRange(formulas);
    context.Customers.AddRange(Customers);
    context.SaveChanges();
  }
}
