using Shared.Common;
using shared.Equipment;
using shared.Formulas;

namespace shared.Quotations;

public static class QuotationResult
{

  public class Index 
  {
    public IEnumerable<QuotationDto.Index>? Quotation { get; set; }

    public int TotalAmount { get; set; }
  }

  public class Detail
  {
    public IEnumerable<FormulaDto.Select> Formulas { get; set; } = default!;
    public IEnumerable<EquipmentDto.Select> Equipment { get; set; } = default!;
    public IEnumerable<DateDto> UnavailableDates { get; set; } = default!;
  }
}
