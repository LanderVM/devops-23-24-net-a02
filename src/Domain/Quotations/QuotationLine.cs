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
  public decimal OriginalEquipmentPrice { get; set; }
  public int AmountOrdered { get; set; }

  public decimal GetPrice()
  {
    return AmountOrdered * OriginalEquipmentPrice;
  }
}
