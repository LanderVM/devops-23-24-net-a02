using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Shared.Quotations;

namespace Domain.Tests;

public class QuotationTests
{
  private List<Equipment> _equipment;
  const string title = "The base food truck formula";

  private static readonly List<string> description =
    new() { "Having a small party? Our iconic food truck is your choice of the evening!" };

  private Formula _formula;
  private Customer _customer;
  private EventLocation _eventLocation;
  private List<QuotationLine> _quotationLines;

  public QuotationTests()
  {
    _equipment = new();
    _formula = new Formula(_equipment, title, description);
    _customer = new Customer("Jan", "Peeters", new Email("JanPeeters@gmail.com"),
      new BillingAddress("Straat", "01", "Zottegem", "9620"), new PhoneNumber("0479254691"), new VatNumber("BE1000000000"));
    _eventLocation = new EventLocation("Straat", "01", "Zottegem", "9620");
    _quotationLines = new();
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  [InlineData(4)]
  [InlineData(7)]
  public void Create_new_quotation_without_extra_equipment_happyFlow(int days)
  {
    var quotation = new Quotation(_formula, _customer, _eventLocation, _quotationLines, DateTime.Now,
      DateTime.Today.AddDays(days), numberOfPeople: 20);

    decimal totalPrice = days > 3
      ? _formula.BasePrice[2] + _formula.PricePerDayExtra * (days - 3)
      : _formula.BasePrice[days - 1];

    quotation.QuotationLines.Count.ShouldBe(0);
    quotation.Status.ShouldBe(QuotationStatus.Open);
    quotation.OriginalFormulaPricePerDay.ShouldBe(_formula.BasePrice);
    quotation.OriginalFormulaPricePerDayExtra.ShouldBe(_formula.PricePerDayExtra);
    quotation.GetPrice().ShouldBe(totalPrice);
    quotation.EventLocation.Street.ShouldBe(_eventLocation.Street);
    quotation.EventLocation.HouseNumber.ShouldBe(_eventLocation.HouseNumber);
    quotation.EventLocation.City.ShouldBe(_eventLocation.City);
    quotation.EventLocation.PostalCode.ShouldBe(_eventLocation.PostalCode);
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(7)]
  [InlineData(9)]
  [InlineData(12)]
  public void Create_new_quotation_with_extra_equipment_happyFlow(int days)
  {
    _equipment.Add(new Equipment("BBQ Deluxe", new List<string> { "Tasty barbecue stuff, in a deluxe package!" }, 100M,
      2));
    _equipment.Add(new Equipment("Tent Decoration",
      new List<string> { "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?" }, 35.99M, 21));
    _quotationLines.Add(new QuotationLine(_equipment[0], 2));
    _quotationLines.Add(new QuotationLine(_equipment[1], 5));

    var quotation = new Quotation(_formula, _customer, _eventLocation, _quotationLines, DateTime.Now,
      DateTime.Today.AddDays(days), numberOfPeople: 20);


    decimal totalPrice = days > 3
      ? _formula.BasePrice[2] + _formula.PricePerDayExtra * (days - 3)
      : _formula.BasePrice[days - 1];
    totalPrice += ((2 * _equipment[0].Price) + (5 * _equipment[1].Price)) * ((days + 2) / 3);

    quotation.QuotationLines.Count.ShouldBe(2);
    quotation.Status.ShouldBe(QuotationStatus.Open);
    quotation.OriginalFormulaPricePerDay.ShouldBe(_formula.BasePrice);
    quotation.OriginalFormulaPricePerDayExtra.ShouldBe(_formula.PricePerDayExtra);
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
        endTime, numberOfPeople: 20);
    });
  }
}
