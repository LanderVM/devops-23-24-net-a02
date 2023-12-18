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
        RuleFor(email => email.Email).NotEmpty().WithMessage(model => "Gelieve een e-mailadres in te vullen")
          .EmailAddress().WithMessage(model => "Gelieve een geldig e-mailadres in te vullen");;
        
      }
    }
  }

  public class Index {
    public string EmailAddress { get; set; }

  }
}
