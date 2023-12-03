﻿using FluentValidation;

namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{
  public PersonalDetailsClass PersonalDetailsObject = new();

  public PersonalDetailsClassValidator personalDetailsClassValidator = new();

  public class PersonalDetailsClass
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string Street { get; set; }
    public string Housenumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }

    public string BtwNumber { get; set; }


  }
  public class PersonalDetailsClassValidator : AbstractValidator<PersonalDetailsClass>
  {
    public PersonalDetailsClassValidator()
    {
      RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
      RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
      RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
      RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(50);

      RuleFor(x => x.Street).NotEmpty().MaximumLength(50);
      RuleFor(x => x.Housenumber).NotEmpty().MaximumLength(15);
      RuleFor(x => x.City).NotEmpty().MaximumLength(50);
      RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(50);

      RuleFor(x => x.BtwNumber).MaximumLength(50);

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
      var result = await ValidateAsync(ValidationContext<PersonalDetailsClass>.CreateWithOptions((PersonalDetailsClass)model, x => x.IncludeProperties(propertyName)));
      if (result.IsValid)
        return Array.Empty<string>();
      return result.Errors.Select(e => e.ErrorMessage);
    };
  }

}
