using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Domain.Formulas;

namespace Domain.Quotations;

public class QuotationLine : Entity
{
  public QuotationLine() { } // EF Core constructor

  public QuotationLine(Equipment equipment, int amountOrdered)
  {
    OriginalEquipmentPrice = equipment.Price;
    EquipmentOrdered = equipment;
    AmountOrdered = amountOrdered;
  }

  public Quotation Quotation { get; set; } = default!;
  public Equipment EquipmentOrdered { get; set; } = default!;
  private decimal _originalEquipmentPricePerDay;

  public decimal OriginalEquipmentPrice
  {
    get => _originalEquipmentPricePerDay;
    set
    {
      _originalEquipmentPricePerDay = Guard.Against.Negative(value);
    }
  }

  public int AmountOrdered { get; set; }

  public decimal GetPrice() // TODO test
  {
    return AmountOrdered * OriginalEquipmentPrice;
  }
}
