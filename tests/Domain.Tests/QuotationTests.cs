using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;

namespace Domain.Tests;

public class QuotationTests
{
  private List<Equipment> equipment;
  const string title = "The base food truck formula";
  const string description = "Having a small party? Our iconic food truck is your choice of the evening!";
  private Formula formula;
  private Customer customer;
  private List<QuotationLine> quotationLines;

  public QuotationTests()
  {
    equipment = new();
    formula = new Formula(equipment, title, description, 20M);
    customer = new Customer("Jan", "Peeters", new Email("JanPeeters@gmail.com"),
      new Address("Straat", "01", "Zottegem", "9620"), new PhoneNumber("0479254691"), "BE123");
    quotationLines = new();
  }

  [Fact]
  public void Create_new_quotation_without_extra_equipment_happyFlow()
  {
    var quotation = new Quotation(formula, customer, customer.BillingAddress, quotationLines, DateTime.Now,
      DateTime.Today.AddDays(3));

    var totalPrice = formula.PricePerDay * 3;

    quotation.QuotationLines.Count.ShouldBe(0);
    quotation.Status.ShouldBe(QuotationStatus.Unread);
    quotation.OriginalFormulaPricePerDay.ShouldBe(formula.PricePerDay);
    quotation.GetPrice().ShouldBe(totalPrice);
  }

  [Fact]
  public void Create_new_quotation_with_extra_equipment_happyFlow()
  {
    equipment.Add(new Equipment("BBQ Deluxe", "Tasty barbecue stuff, in a deluxe package!", 100M, 2));
    equipment.Add(new Equipment("Tent Decoration",
      "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?", 35.99M, 21));
    quotationLines.Add(new QuotationLine(equipment[0], 2));
    quotationLines.Add(new QuotationLine(equipment[1], 5));

    var quotation = new Quotation(formula, customer, customer.BillingAddress, quotationLines, DateTime.Now,
      DateTime.Today.AddDays(1));

    var totalPrice = formula.PricePerDay + (2 * equipment[0].Price) + (5 * equipment[1].Price) * 1;

    quotation.QuotationLines.Count.ShouldBe(2);
    quotation.Status.ShouldBe(QuotationStatus.Unread);
    quotation.OriginalFormulaPricePerDay.ShouldBe(formula.PricePerDay);
    quotation.GetPrice().ShouldBe(totalPrice);
  }

  [Fact]
  public void Create_new_quotation_timeRange_invalid()
  {
    var startTime = DateTime.Now;
    var endTime = DateTime.Now.AddDays(-2);

    Should.Throw<ArgumentException>(() =>
    {
      new Quotation(formula, customer, customer.BillingAddress, quotationLines, startTime,
        endTime);
    });
  }
}
