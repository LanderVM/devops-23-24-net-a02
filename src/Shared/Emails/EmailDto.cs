using FluentValidation;

namespace devops_23_24_net_a02.Shared.DTOs;

public abstract class EmailDto
{
  public class CreateEmail
  {
    public string Email { get; set; }
  }

  public class Validator : AbstractValidator<CreateEmail>
  {
    public Validator()
    {
      RuleFor(email => email.Email).NotEmpty().EmailAddress();
      RuleFor(email => email.Email).Must(x => !x.EndsWith(".")).WithMessage("'Email' may not end on a period.");
    }
  }
}
