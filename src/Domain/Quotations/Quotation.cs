using Domain.Common;
using Domain.Customers;
using Domain.Formulas;

namespace Domain.Quotations;

public class Quotation : Entity
{
  private Quotation() { } // EF Core Constructor

  public Quotation(Formula formula, Customer orderedBy, EventLocation eventLocation, List<QuotationLine> quotationLines,
    DateTime startTime, DateTime endTime, bool isTripelBier = false)
  {
    if ((endTime - startTime).TotalSeconds <= 0)
      throw new ArgumentException("End time cannot be before start time!");

    Formula = Guard.Against.Null(formula);
    OriginalFormulaPricePerDay = formula.BasePrice;
    OriginalFormulaPricePerDayExtra = formula.PricePerDayExtra;
    OrderedBy = Guard.Against.Null(orderedBy);
    EventLocation = Guard.Against.Null(eventLocation);
    QuotationLines = Guard.Against.Null(quotationLines);
    Status = QuotationStatus.Unread;
    StartTime = Guard.Against.Null(startTime);
    EndTime = Guard.Against.Null(endTime);
    IsTripelBier = isTripelBier;
  }

  public Formula Formula { get; set; } = default!;
  public List<decimal> OriginalFormulaPricePerDay { get; protected set; } = new();
  public decimal OriginalFormulaPricePerDayExtra { get; protected set; }

  public Customer OrderedBy { get; set; } = default!;
  public EventLocation EventLocation { get; set; } = default!;
  public List<QuotationLine> QuotationLines { get; set; } = new();
  public QuotationStatus Status { get; set; } = QuotationStatus.Unread;
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public bool IsTripelBier { get; set; }

  public decimal GetPrice()
  {
    var days = (EndTime - StartTime).Days + 1;
    var hasExtraDays = days > 3;

    var basePrice = OriginalFormulaPricePerDay[hasExtraDays ? 3 : days];
    decimal extraDaysPrice = 0;
    if (hasExtraDays) extraDaysPrice = (days - 3) * OriginalFormulaPricePerDayExtra;

    var blocksOf3Days = days / 3 + (days % 3 != 0 ? 1 : 0);
    var extraEquipmentPrices = QuotationLines.Sum(quotationLine => quotationLine.GetPrice() * blocksOf3Days);

    return basePrice + extraDaysPrice + extraEquipmentPrices;
  }
}
