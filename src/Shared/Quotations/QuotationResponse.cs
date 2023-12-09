using FluentValidation;
using Shared.Common;
using Shared.Customer;
using shared.Equipment;

namespace shared.Quotations;

public static class QuotationResponse
{
  public class Create
  {
    public int QuotationId { get; set; } = default!;
    public int FormulaId { get; set; } = default!;
    public AddressDto EventLocation { get; set; } = default!; // TODO
    public DateTime StartTime { get; set; } = default!;
    public DateTime EndTime { get; set; } = default!;
    public List<EquipmentDto.Lines> Equipments { get; set; } = default!;
    public CustomerDto.Details Customer { get; set; } = default!;
    public Boolean IsTripelBier { get; set; } = default!;
    public int NumberOfPeople { get; set; } = default!;
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
