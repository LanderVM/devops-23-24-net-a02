using FluentValidation;
using shared.Common;
using Shared.Customer;
using shared.Equipment;
using shared.Formulas;
using Shared.Quotations;

namespace shared.Quotations;

public static class QuotationDto
{
  public class Index
  {
    public int QuotationId { get; set; }
    public CustomerDto.Index Customer { get; set; } = default!;
    public String CreatedAt { get; set; } = default!;
  }

  public class DetailEdit
  {
    public int QuotationId { get; set; }
    public FormulaDto.Select Formula { get; set; } = default!;
    public CustomerDto.Details Customer { get; set; } = default!;
    public AddressDto EventLocation { get; set; } = default!;
    public IEnumerable<EquipmentDto.LinesDetail> Equipment { get; set; } = default!;
    public bool IsTripelBier { get; set; }
    public decimal NumberOfPeople { get; set; }
    public string? Opmerking { get; set; }
    public QuotationStatus Status { get; set; }
  }

  public class Dates
  {
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }

  public class Estimate
  {
    public int FormulaId { get; set; }
    public List<int>? EquipmentIds { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int EstimatedNumberOfPeople { get; set; }
    public bool IsTripelBier { get; set; }

    public class Validator : AbstractValidator<Estimate>
    {
      public Validator()
      {
        RuleFor(model => model.FormulaId).NotEmpty().WithMessage("Formule id mag niet leeg zijn!");
        RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formule id moet een positief getal zijn!");
        RuleFor(model => model.StartTime).NotEmpty();
        RuleFor(model => model.EndTime).NotEmpty();
        RuleFor(model => new { model.StartTime, model.EndTime })
          .Must(model => model.EndTime - model.StartTime >= 0)
          .WithMessage("De begin tijd kan niet starten achter de eind tijd!");
        RuleFor(model => model.EstimatedNumberOfPeople).GreaterThan(0)
          .WithMessage("Het verwacht aantal personen kan niet minder dan 0 zijn!");
      }
    }
  }

  public class Edit
  {
    public string? Opmerking { get; set; }
    public IEnumerable<EquipmentDto.LinesDetail> EquipmentList { get; set; } = default!;
    public bool IsTripelBier { get; set; }
    public bool IsAccepted { get; set; }
  }

  public class Create
  {
    public int FormulaId { get; set; }
    public AddressDto EventLocation { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<EquipmentDto.Lines> Equipments { get; set; } = default!;
    public CustomerDto.Create Customer { get; set; } = default!;
    public bool IsTripelBier { get; set; }

    public int NumberOfPeople { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.FormulaId).NotEmpty().WithMessage("Formule id mag niet leeg zijn!");
      RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formule id moet een positief getal zijn!");
      RuleFor(model => model.EventLocation).NotEmpty();
      RuleFor(model => model.StartTime).NotEmpty().WithMessage(_ => "Gelieve een startdatum in te vullen");
      RuleFor(model => model.EndTime).NotEmpty().WithMessage(_ => "Gelieve een einddatum in te vullen");
      RuleFor(model => new { model.StartTime, model.EndTime })
        .Must(model => (model.EndTime - model.StartTime).TotalSeconds >= 0)
        .WithMessage("Einddatum mag niet voor startdatum zijn!");
      RuleFor(model => model.Customer).NotEmpty();
    }
  }
}
