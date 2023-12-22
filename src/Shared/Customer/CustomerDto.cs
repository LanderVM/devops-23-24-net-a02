
using System.Text.RegularExpressions;
using devops_23_24_net_a02.Shared.Emails;
using FluentValidation;
using shared.Common;

namespace Shared.Customer;

public static class CustomerDto
{
  public class Index
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public String Email { get; set; } = default!;
  }
  public class Details
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public EmailDto.Create Email { get; set; }
    public AddressDto BillingAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? VatNumber { get; set; }
  }
  public class Create
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmailDto.Create Email { get; set; }
    public AddressDto BillingAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string? VatNumber { get; set; }

    public class Validator : AbstractValidator<Create>
    {
      public Validator()
      {
        RuleFor(model => model.FirstName).NotEmpty().WithMessage(model => "Gelieve een voornaam in te vullen")
          .MaximumLength(200).WithMessage(model => "Gelieve een voornaam in te vullen");
        RuleFor(model => model.LastName).NotEmpty().WithMessage(model => "Gelieve een achternaam in te vullen")
          .MaximumLength(200).WithMessage(model => "Gelieve een geldig achternaam in te vullen");
        RuleFor(model => model.Email).NotEmpty().SetValidator(new EmailDto.Create.Validator());
        RuleFor(model => model.BillingAddress).NotEmpty().SetValidator(new AddressDto.Validator());
        RuleFor(model => model.PhoneNumber)
          .NotEmpty().WithMessage(model => "Gelieve een telefoonnummer in te vullen")
          .Matches(@"^(\+32\s?|0)4[56789]\d{7}$").WithMessage("Gelieve een geldig telefoonnummer in te voeren")
          .MaximumLength(12).WithMessage(model => "Gelieve geen spaties in te voeren");
        When(model => !string.IsNullOrEmpty(model.VatNumber), () =>
        {
          RuleFor(model => model.VatNumber)
            .Matches("^BE[01][0-9]{9}$")
            .WithMessage(model =>"De eerste twee letters van het btw-nummer moeten de Belgische landcode BE zijn gevolgd door het cijfer 0 of 1, daarna moeten er 9 cijfers volgen.").MaximumLength(200);
        });
      }
    }
  }
}

