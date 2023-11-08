using Domain.Common;
using Domain.Customers;
using Domain.Formulas;

namespace Domain.Quotations;

public class Quotation : Entity
{
  private Quotation() { } // EF Core Constructor

  public Quotation(Formula formula, Customer orderedBy, Address eventLocation, List<QuotationLine> quotationLines,
    DateTime startTime, DateTime endTime)
  {
    Formula = formula;
    OriginalFormulaPricePerDay = formula.PricePerDay;
    OrderedBy = orderedBy;
    EventLocation = eventLocation;
    QuotationLines = quotationLines;
    Status = QuotationStatus.Unread;
    StartTime = startTime;
    EndTime = endTime;
  }

  public Formula Formula { get; set; } = default!;
  public decimal OriginalFormulaPricePerDay { get; set; }
  public Customer OrderedBy { get; set; } = default!;
  public Address EventLocation { get; set; } = default!;
  public List<QuotationLine> QuotationLines { get; set; } = new();
  public QuotationStatus Status { get; set; } = QuotationStatus.Unread;
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }

  public decimal GetPrice()
  {
    var days = (EndTime - StartTime).Days;
    var basePrice = OriginalFormulaPricePerDay * days;
    var equipmentPrices = QuotationLines.Sum(quotationLine => quotationLine.GetPrice() * days);
    return basePrice + equipmentPrices;
  }
}
