using Domain.Common;
using Domain.Formulas;

namespace Api.Data;

public static class DbInitializer
{
  public static void Initialize(BlancheDbContext context)
  {
    if (context.Formulas.Any()
        && context.Equipments.Any())
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
      new(new List<Equipment> { bbq, tent, barrel }, "Extended", "For the best party you could imagine")
    };


    context.Formulas.AddRange(formulas);
    context.SaveChanges();
  }
}
