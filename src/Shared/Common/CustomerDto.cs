using devops_23_24_net_a02.Shared.DTOs;
using Domain.Customers;
using FluentValidation;

namespace Shared.Common;

public static class CustomerDto
{
  public class Index
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public String Email { get; set; }
  }
  public class Create
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmailDto.Create Email { get; set; }
    public AddressDto BillingAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string? VatNumber { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.FirstName).NotEmpty();
      RuleFor(model => model.LastName).NotEmpty();
      RuleFor(model => model.Email).NotEmpty();
      RuleFor(model => model.BillingAddress).NotEmpty();
      RuleFor(model => model.PhoneNumber).NotEmpty();
      RuleFor(model => model.VatNumber)
        .Must(vat => vat.Substring(0, 2).Any(char.IsLetter))
        .WithMessage("First two letters of VAT number must be your country code!");
    }
  }
}
