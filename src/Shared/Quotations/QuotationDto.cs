using FluentValidation;
using Shared.Common;

namespace Shared.Quotations;

public abstract class QuotationDto
{
  public abstract class Create
  {
    public AddressDto EventLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public CustomerDto.Create Customer { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.EventLocation).NotEmpty();
      RuleFor(model => model.StartTime).NotEmpty();
      RuleFor(model => model.EndTime).NotEmpty();
      RuleFor(model => new { model.StartTime, model.EndTime })
        .Must(model => (model.EndTime - model.StartTime).TotalSeconds > 0)
        .WithMessage("End time cannot be before start time!");
      RuleFor(model => model.Customer).NotEmpty();
    }
  }
}
