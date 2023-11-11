using Domain.Common;
using Domain.Customers;
using Domain.Formulas;

namespace Domain.Quotations;

public class Quotation : Entity
{
  private Quotation() { } // EF Core Constructor

  public Quotation(Formula formula, Customer orderedBy, EventLocation eventLocation, List<QuotationLine> quotationLines,
    DateTime startTime, DateTime endTime)
  {
    if ((endTime - startTime).TotalSeconds <= 0) 
      throw new ArgumentException("End time cannot be before start time!");

    Formula = Guard.Against.Null(formula);
    OriginalFormulaPricePerDay = formula.PricePerDay;
    OrderedBy = Guard.Against.Null(orderedBy);
    EventLocation = Guard.Against.Null(eventLocation);
    QuotationLines = Guard.Against.Null(quotationLines);
    Status = QuotationStatus.Unread;
    StartTime = Guard.Against.Null(startTime);
    EndTime = Guard.Against.Null(endTime);
  }

  public Formula Formula { get; set; } = default!;
  private decimal _originalFormulaPricePerDay;

  public decimal OriginalFormulaPricePerDay
  {
    get => _originalFormulaPricePerDay;
    set
    {
      _originalFormulaPricePerDay = Guard.Against.Negative(value);
    }
  }

  public Customer OrderedBy { get; set; } = default!;
  public EventLocation EventLocation { get; set; } = default!;
  public List<QuotationLine> QuotationLines { get; set; } = new();
  public QuotationStatus Status { get; set; } = QuotationStatus.Unread;
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }

  public decimal GetPrice()
  {
    var days = (EndTime - StartTime).Days + 1;
    var basePrice = OriginalFormulaPricePerDay * days;
    var extraEquipmentPrices = QuotationLines.Sum(quotationLine => quotationLine.GetPrice() * days);
    return basePrice + extraEquipmentPrices;
  }
}
