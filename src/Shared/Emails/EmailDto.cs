using FluentValidation;
using shared.Quotations;

namespace devops_23_24_net_a02.Shared.Emails;

public static class EmailDto
{
  public class Create
  {
    public string Email { get; set; }

    public class Validator : AbstractValidator<Create>
    {
      public Validator()
      {
        RuleFor(email => email.Email).NotEmpty().EmailAddress();
        //RuleFor(email => email.Email).Must(x => !x.EndsWith(".")).WithMessage("'Email' may not end on a period.");
      }
    }
  }

  
}
