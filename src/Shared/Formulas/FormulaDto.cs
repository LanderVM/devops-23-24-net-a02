using Domain.Formulas;
using FluentValidation;
using Shared.Common;

namespace shared.Formulas;
public class FormulaDto {

  public class Index {
    public int Id { get; set; }

    public string Title { get; set; }

    public List<string> Attributes { get; set;}

    public decimal PricePerDayExtra { get; set; }

    public List<decimal> BasePrice { get; set; }
    
    public bool IsActive { get; set; }

  }

  public class Select
  {
    public int Id { get; set; }

    public string Title { get; set; }
  }
  
  public class Mutate {
    
    public string Title { get; set; }
    public string Attributes { get; set;}
    public decimal PricePerDayExtra { get; set; }
    public string BasePrice { get; set; }
    public bool IsActive { get; set; }

    public class Validator : AbstractValidator<FormulaDto.Mutate>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty().WithMessage("De titel mag niet leeg zijn")
          .MaximumLength(100).WithMessage("Gelieve een kortere titel in te geven");
        RuleFor(x => x.Attributes).NotEmpty().WithMessage("De attributen mogen niet leeg zijn")
          .MaximumLength(200).WithMessage("Dit zijn te veel attributen");
        RuleFor(x => x.PricePerDayExtra).NotEmpty().WithMessage("De prijs mag niet leeg zijn")
          .InclusiveBetween(0, 5000).WithMessage("De prijs moet een getal tussen 0 en 5000 zijn");
        RuleFor(x => x.BasePrice).NotEmpty().WithMessage("De prijs mag niet leeg zijn");

      }

    }
  }
}

