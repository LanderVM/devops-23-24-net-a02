using Domain.Customers;
using FluentValidation;
using shared.Equipment;
using shared.Formulas;
using Shared.Common;
using Shared.Customer;

namespace Shared.Quotations;

public static class QuotationDto
{

  public class Index
  {
    public int QuotationId { get; set; }
    public CustomerDto.Index Customer { get; set; }
    public String CreatedAt { get; set; }
    
  }
  public class Create
  {
    public int FormulaId { get; set; }
    public AddressDto EventLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public CustomerDto.Create Customer { get; set; }
    public bool IsTripelBier { get; set; }
  }

  public class Estimate
  {
    public int FormulaId { get; set; }
    public AddressDto? EventLocation { get; set; } = default!;
    public List<int>? EquipmentIds { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EstimatedNumberOfPeople { get; set; }
    public bool IsTripelBier { get; set; }
  }

  public class Details
  {
    public IEnumerable<FormulaDto.Select> Formulas { get; set; } = default!;
    public IEnumerable<EquipmentDto.Select> Equipment { get; set; } = default!;
    public IEnumerable<DateDto> UnavailableDates { get; set; } = default!;
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.FormulaId).NotEmpty();
      RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formula id must be a positive id!");
      RuleFor(model => model.EventLocation).NotEmpty();
      RuleFor(model => model.StartTime).NotEmpty();
      RuleFor(model => model.EndTime).NotEmpty();
      RuleFor(model => new { model.StartTime, model.EndTime })
        .Must(model => (model.EndTime - model.StartTime).TotalSeconds > 0)
        .WithMessage("End time cannot be before start time!");
      RuleFor(model => model.Customer).NotEmpty();
      RuleFor(model => model.IsTripelBier).NotEmpty();
    }
  }
}
