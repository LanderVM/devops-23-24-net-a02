using FluentValidation;

namespace Shared.Common;

public abstract class AddressDto
{
  public abstract class Create
  {
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.Street).NotEmpty();
      RuleFor(model => model.HouseNumber).NotEmpty();
      RuleFor(model => model.PostalCode).NotEmpty();
      RuleFor(model => model.City).NotEmpty();
    }
  }
}
