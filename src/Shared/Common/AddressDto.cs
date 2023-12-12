using FluentValidation;

namespace Shared.Common;

public class AddressDto
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

  public class Validator : AbstractValidator<AddressDto>
  {
    public Validator()
    {
      RuleFor(model => model.Street).NotEmpty().MaximumLength(200);
      RuleFor(model => model.HouseNumber).NotEmpty().MaximumLength(200);
      RuleFor(model => model.PostalCode).NotEmpty().MaximumLength(200);
      RuleFor(model => model.City).NotEmpty().MaximumLength(200);
    }
  }
}
