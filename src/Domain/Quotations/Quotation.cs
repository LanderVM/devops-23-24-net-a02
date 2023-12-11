using Domain.Common;
using Domain.Customers;
using Domain.Formulas;

namespace Domain.Quotations;

public class Quotation : Entity
{
  private Quotation() { } // EF Core Constructor

  public Quotation(Formula formula, Customer orderedBy, EventLocation eventLocation, List<QuotationLine> quotationLines,
    DateTime startTime, DateTime endTime, bool isTripelBier = false, int numberOfPeople = 0) // TODO numberofpeople nooit null
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
    NumberOfPeople = Guard.Against.NegativeOrZero(numberOfPeople);
    IsTripelBier = isTripelBier;
  }
  public Quotation(Formula formula, int formulaId, DateTime startTime, DateTime endTime, int estimatedNumberPeople, bool isTripelBier = false)
  { // todo ??
    if ((endTime - startTime).TotalSeconds <= 0)
      throw new ArgumentException("End time cannot be before start time!");

    Formula = Guard.Against.Null(formula);
    FormulaId = formulaId;
    OriginalFormulaPricePerDay = formula.BasePrice;
    OriginalFormulaPricePerDayExtra = formula.PricePerDayExtra;
    StartTime = Guard.Against.Null(startTime);
    EndTime = Guard.Against.Null(endTime);
    NumberOfPeople = Guard.Against.NegativeOrZero(estimatedNumberPeople);
    IsTripelBier = isTripelBier;
  }
  
  public Formula Formula { get; set; } = default!;
  public int FormulaId { get; set; } = 1;
  public List<decimal> OriginalFormulaPricePerDay { get; protected set; } = new();
  public decimal OriginalFormulaPricePerDayExtra { get; protected set; }

  public Customer OrderedBy { get; set; } = default!;
  public EventLocation EventLocation { get; set; } = default!;
  public List<QuotationLine> QuotationLines { get; set; } = new();
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public int NumberOfPeople { get; protected set; }
  public bool IsTripelBier { get; set; }

  public QuotationStatus Status { get; set; } = QuotationStatus.Unread;
  public string? Opmerking { get; set; } = default!;

  public decimal GetPrice()
  {
    var days = (EndTime - StartTime).Days + 1;
    var blocksOf3Days = days / 3 + (days % 3 != 0 ? 1 : 0);
    var extraEquipmentPrices = QuotationLines.Sum(quotationLine => quotationLine.GetPrice() * blocksOf3Days);

    return GetPriceDays() + extraEquipmentPrices;
  }
  private decimal GetPriceDays()
  {
    var days = (EndTime - StartTime).Days + 1;
    var hasExtraDays = days > 3;

    var basePrice = OriginalFormulaPricePerDay[hasExtraDays ? 2 : days - 1];
    decimal extraDaysPrice = 0;
    if (hasExtraDays) extraDaysPrice = (days - 3) * OriginalFormulaPricePerDayExtra;

    return basePrice + extraDaysPrice;
  }

  public decimal GetEstimatedPrice()
  {
    decimal priceBeer = IsTripelBier ? 3.0m : 1.5m;
    decimal priceBbq = 12m;

    if (FormulaId == 3)
    {
      return GetPriceDays() + Formula.getPriceForEquipment() + (NumberOfPeople * priceBeer) + (NumberOfPeople * priceBbq);
    }
    if (FormulaId == 2)
    {
      return GetPriceDays() + Formula.getPriceForEquipment() + (NumberOfPeople * priceBeer);
    }
    else
    {
      return GetPriceDays() + Formula.getPriceForEquipment();
    }
  }
}
