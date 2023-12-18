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
      RuleFor(model => model.Street).NotEmpty().WithMessage(model => "Gelieve een straat in te vullen")
        .MaximumLength(200).WithMessage(model => "Gelieve een geldige straat in te vullen");
      RuleFor(model => model.HouseNumber).NotEmpty().WithMessage(model => "Gelieve een huisnummer in te vullen")
        .MaximumLength(200).WithMessage(model => "Gelieve een geldig huisnummer in te vullen");
      RuleFor(model => model.PostalCode).NotEmpty().WithMessage(model => "Gelieve een postcode in te vullen")
        .MaximumLength(200).WithMessage(model => "Gelieve een geldige postcode in te vullen");
      RuleFor(model => model.City).NotEmpty().WithMessage(model => "Gelieve een stad in te vullen")
        .MaximumLength(200).WithMessage(model => "Gelieve een geldige stad in te vullen");
    }
  }
}
