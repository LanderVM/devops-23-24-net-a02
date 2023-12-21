using Shared.Common;
using Shared.Customer;
using shared.Equipment;
using shared.Formulas;
using Domain.Quotations;

namespace shared.Quotations;

public static class QuotationResult
{
  public class Index
  {
    public IEnumerable<QuotationDto.Index>? Quotation { get; set; }
    public int TotalAmount { get; set; }
  }

  public class DetailEdit
  {
    public QuotationDto.DetailEdit Quotation { get; set; } = default!;
  }

  public class Dates
  {
    public IEnumerable<QuotationDto.Dates>? DateRanges { get; set; }
  }

  public class Detail
  {
    public IEnumerable<FormulaDto.Select> Formulas { get; set; } = default!;
    public IEnumerable<EquipmentDto.Select> Equipment { get; set; } = default!;
    public IEnumerable<DateDto> UnavailableDates { get; set; } = default!;
  }

    public class Create
    {
      public int QuotationId { get; set; } = default!;
      public int FormulaId { get; set; } = default!;
      public AddressDto EventLocation { get; set; } = default!;
      public DateTime StartTime { get; set; } = default!;
      public DateTime EndTime { get; set; } = default!;
      public List<EquipmentDto.Lines> Equipments { get; set; } = default!;
      public CustomerDto.Details Customer { get; set; } = default!;
      public Boolean IsTripelBier { get; set; } = default!;
      public int NumberOfPeople { get; set; } = default!;
    }
}
