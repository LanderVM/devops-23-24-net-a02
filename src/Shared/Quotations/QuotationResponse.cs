using FluentValidation;
using Shared.Common;

namespace shared.Quotations;

public static class QuotationResponse
{
  public class Create
  {
    public int QuotationId { get; set; }
  }
  public class Price
  {
    public decimal EstimatedPrice { get; set; }
  }

  public class Estimate
  {
    public int FormulaId { get; set; }
    public List<int>? EquipmentIds { get; set; } = default!;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int EstimatedNumberOfPeople { get; set; }
    public bool IsTripelBier { get; set; } = false;
  }
}
