using FluentValidation;
using Shared.Common;
using Shared.Customer;
using shared.Equipment;
using Domain.Quotations;

namespace shared.Quotations;

public static class QuotationResponse
{
  public class Create
  {
    public int QuotationId { get; set; }
    public int FormulaId { get; set; }
    public AddressDto EventLocation { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<EquipmentDto.Lines> Equipments { get; set; } = default!;
    public CustomerDto.Details Customer { get; set; } = default!;
    public Boolean IsTripelBier { get; set; }
    public int NumberOfPeople { get; set; } = default!;
    public string? Opmerking {  get; set; }
    public QuotationStatus Status { get; set; }
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

  public class Edit
  {
    public int QuotationId { get; set; }
  }
}
