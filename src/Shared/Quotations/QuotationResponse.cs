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
    public int QuotationId { get; set; } = default!;
    public int FormulaId { get; set; } = default!;
    public AddressDto EventLocation { get; set; } = default!;
    public DateTime StartTime { get; set; } = default!;
    public DateTime EndTime { get; set; } = default!;
    public List<EquipmentDto.Lines> Equipments { get; set; } = default!;
    public CustomerDto.Details Customer { get; set; } = default!;
    public Boolean IsTripelBier { get; set; } = default!;
    public int NumberOfPeople { get; set; } = default!;
    public string? Opmerking { get; set; }
    public QuotationStatus Status { get; set; }
  }

  public class Price
  {
    public decimal EstimatedPrice { get; set; }
  }

  public class Edit
  {
    public int QuotationId { get; set; }
  }
}
