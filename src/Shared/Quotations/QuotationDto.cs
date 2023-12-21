using Domain.Quotations;
using FluentValidation;
using Shared.Common;
using Shared.Customer;
using Shared.Customer;
using shared.Equipment;
using shared.Formulas;
using Domain.Common;

namespace shared.Quotations;

public static class QuotationDto
{

  public class Index
  {
    public int QuotationId { get; set; } 
    public CustomerDto.Index Customer { get; set; }
    public String CreatedAt { get; set; }
    
  }

  public class DetailEdit
  {
    public int QuotationId { get; set; }
    public FormulaDto.Select Formula { get; set; } = default!;
    public CustomerDto.Details Customer { get; set; } = default!;
    public EventLocation EventLocation { get; set; } = default!;
    public IEnumerable<EquipmentDto.Lines> Equipment { get; set; } = default!;
    public bool IsTripelBier { get; set; }
    public decimal NumberOfPeople { get; set; }
    public string? Opmerking { get; set; }
    public QuotationStatus Status { get; set; }
  }

  public class Dates { 
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }

  public class Estimate
  {
    public int FormulaId { get; set; }
    public List<int>? EquipmentIds { get; set; } = default!;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int EstimatedNumberOfPeople { get; set; }
    public bool IsTripelBier { get; set; } = false;

    public class Validator : AbstractValidator<Estimate>
    {
      public Validator()
      {
        RuleFor(model => model.FormulaId).NotEmpty().WithMessage("Formule id mag niet leeg zijn!");
        RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formule id moet een positief getal zijn!");
        RuleFor(model => model.StartTime).NotEmpty();
        RuleFor(model => model.EndTime).NotEmpty();
        RuleFor(model => new { model.StartTime, model.EndTime })
          .Must(model => (model.EndTime - model.StartTime) >= 0)
          .WithMessage("De begin tijd kan niet starten achter de eind tijd!");
        RuleFor(model => model.IsTripelBier).Must(IsTripelBier => IsTripelBier == false || IsTripelBier == true).WithMessage("De keuze voor tripel bier moet aangevuld zijn!");
        RuleFor(model => model.EstimatedNumberOfPeople).GreaterThan(0).WithMessage("Het verwacht aantal personen kan niet minder dan 0 zijn!");
      }
    }
  }

  public class Edit
  {
    public string? Opmerking { get; set; } = default!;
    public QuotationStatus Status { get; set; }
    public IEnumerable<EquipmentDto.Lines>? EquipmentList { get; set; } = default!;

    public class Validator : AbstractValidator<Edit>
    {
      public Validator()
      {
        RuleFor(model => model.Status).NotEmpty().WithMessage("Geef een geldige status!");   
      }
    }
  }

  public class Create
  {
    public int FormulaId { get; set; }
    public AddressDto EventLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public List<EquipmentDto.Lines> Equipments { get; set; }
    public CustomerDto.Create Customer { get; set; }
    public bool IsTripelBier { get; set; } = default!;
    
    public int NumberOfPeople { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.FormulaId).NotEmpty().WithMessage("Formule id mag niet leeg zijn!");
      RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formule id moet een positief getal zijn!");
      RuleFor(model => model.EventLocation).NotEmpty();
      RuleFor(model => model.StartTime).NotEmpty().WithMessage(model => "Gelieve een startdatum in te vullen"); 
      RuleFor(model => model.EndTime).NotEmpty().WithMessage(model => "Gelieve een einddatum in te vullen");
      RuleFor(model => new { model.StartTime, model.EndTime })
        .Must(model => (model.EndTime - model.StartTime).TotalSeconds > 0)
        .WithMessage("End time cannot be before start time!");
      RuleFor(model => model.Customer).NotEmpty();
    
    }
  }
}
