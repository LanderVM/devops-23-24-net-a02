﻿
using devops_23_24_net_a02.Shared.Emails;
using FluentValidation;
using Shared.Common;

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

    public Create() { 
      Email = new EmailDto.Create();
      BillingAddress = new AddressDto();
    }

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
        RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage(model => "Gelieve een telefoonnummer in te vullen")
          .Matches("0[1-9][0-9]{8}").MaximumLength(10).WithMessage(model => "Gelieve een geldig telefoonnummer, zonder spaties, in te voeren!");
        When(model => !string.IsNullOrEmpty(model.VatNumber), () =>
        {
          RuleFor(model => model.VatNumber).Matches("[B][E][0-9]+").WithMessage(model =>"De eerste twee letters van het btw-nummer moeten uw landcode zijn, daarna moeten er cijfers volgen!").MaximumLength(200);
        });
      }
    }
  }

}
