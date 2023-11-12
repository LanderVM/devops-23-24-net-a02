using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;

namespace Domain.Tests;

public class QuotationTests
{
  private List<Equipment> _equipment;
  const string title = "The base food truck formula";
  const string description = "Having a small party? Our iconic food truck is your choice of the evening!";
  private Formula _formula;
  private Customer _customer;
  private EventLocation _eventLocation;
  private List<QuotationLine> _quotationLines;

  public QuotationTests()
  {
    _equipment = new();
    _formula = new Formula(_equipment, title, description, 20M);
    _customer = new Customer("Jan", "Peeters", new Email("JanPeeters@gmail.com"),
      new BillingAddress("Straat", "01", "Zottegem", "9620"), new PhoneNumber("0479254691"), "BE123");
    _eventLocation = new EventLocation("Straat", "01", "Zottegem", "9620");
    _quotationLines = new();
  }

  [Fact]
  public void Create_new_quotation_without_extra_equipment_happyFlow()
  {
    var quotation = new Quotation(_formula, _customer, _eventLocation, _quotationLines, DateTime.Now,
      DateTime.Today.AddDays(3));

    var totalPrice = _formula.PricePerDay * 3;

    quotation.QuotationLines.Count.ShouldBe(0);
    quotation.Status.ShouldBe(QuotationStatus.Unread);
    quotation.OriginalFormulaPricePerDay.ShouldBe(_formula.PricePerDay);
    quotation.GetPrice().ShouldBe(totalPrice);
  }

  [Fact]
  public void Create_new_quotation_with_extra_equipment_happyFlow()
  {
    _equipment.Add(new Equipment("BBQ Deluxe", "Tasty barbecue stuff, in a deluxe package!", 100M, 2));
    _equipment.Add(new Equipment("Tent Decoration",
      "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?", 35.99M, 21));
    _quotationLines.Add(new QuotationLine(_equipment[0], 2));
    _quotationLines.Add(new QuotationLine(_equipment[1], 5));

    var quotation = new Quotation(_formula, _customer, _eventLocation, _quotationLines, DateTime.Now,
      DateTime.Today.AddDays(1));

    var totalPrice = _formula.PricePerDay + (2 * _equipment[0].Price) + (5 * _equipment[1].Price) * 1;

    quotation.QuotationLines.Count.ShouldBe(2);
    quotation.Status.ShouldBe(QuotationStatus.Unread);
    quotation.OriginalFormulaPricePerDay.ShouldBe(_formula.PricePerDay);
    quotation.GetPrice().ShouldBe(totalPrice);
  }

  [Fact]
  public void Create_new_quotation_timeRange_invalid()
  {
    var startTime = DateTime.Now;
    var endTime = DateTime.Now.AddDays(-2);

    Should.Throw<ArgumentException>(() =>
    {
      new Quotation(_formula, _customer, _eventLocation, _quotationLines, startTime,
        endTime);
    });
  }
}
