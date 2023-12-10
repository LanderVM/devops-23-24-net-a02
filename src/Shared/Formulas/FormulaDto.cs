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

    public class Validator : AbstractValidator<FormulaDto.Mutate>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Attributes).NotEmpty();
        RuleFor(x => x.PricePerDayExtra).NotEmpty().InclusiveBetween(0, 5000);
        RuleFor(x => x.BasePrice).NotEmpty();

      }

    }
  }
}

