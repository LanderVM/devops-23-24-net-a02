using FluentValidation;

namespace shared.Equipment;

public static class EquipmentDto
{
  public class Index
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ImageData ImageData { get; set; }
    public List<int>? FormulaIds { get; set; }

    public bool IsActive { get; set; }
  }

  public class Lines
  {
    public int EquipmentId { get; set; }
    public int Amount { get; set; }

    public class Validator : AbstractValidator<Lines>
    {
      public Validator()
      {
        RuleFor(x => x.Amount).NotEmpty();
      }
    }
  }

  public class LinesDetail
  {
    public int EquipmentId { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = default!;

    public class Validator : AbstractValidator<Lines>
    {
      public Validator()
      {
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Het aantal moet meer zijn dan 0");
      }
    }
  }

  public class ImageData
  {
    public string ImageUrl { get; set; }

    public string AltText { get; set; }
  }

  public class Select
  {
    public int Id { get; set; }
    public string Title { get; set; } = default!;
  }

  public class Ids
  {
    public List<int> Id { get; set; } = default!;
  }

  public class Create
  {
    public string Title { get; set; }
    public string Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageContentType { get; set; }
    public bool IsActive { get; set; }


    public class Validator : AbstractValidator<Create>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty().WithMessage("De titel mag niet leeg zijn")
          .MaximumLength(100).WithMessage("Gelieve een kortere titel in te geven");
        RuleFor(x => x.Attributes).NotEmpty().WithMessage("De attributen mogen niet leeg zijn")
          .MaximumLength(200).WithMessage("Dit zijn te veel attributen");
        ;
        RuleFor(x => x.Price).NotEmpty().WithMessage("De prijs mag niet leeg zijn")
          .InclusiveBetween(0, 5000).WithMessage("De prijs moet een getal tussen 0 en 5000 zijn");
        RuleFor(x => x.Stock).NotEmpty().WithMessage("De voorraad mag niet leeg zijn")
          .InclusiveBetween(1, 1000).WithMessage("De voorraad moet een getal tussen 1 en 1000 zijn");
      }
    }
  }

  public class Mutate
  {
    public string Title { get; set; }
    public string Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageContentType { get; set; }
    public ImageData ImageData { get; set; }

    public bool IsActive { get; set; }

    public class Validator : AbstractValidator<Mutate>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty().WithMessage("De titel mag niet leeg zijn")
          .MaximumLength(100).WithMessage("Gelieve een kortere titel in te geven");
        RuleFor(x => x.Attributes).NotEmpty().WithMessage("De attributen mogen niet leeg zijn")
          .MaximumLength(200).WithMessage("Dit zijn te veel attributen");
        RuleFor(x => x.Price).NotEmpty().WithMessage("De prijs mag niet leeg zijn")
          .InclusiveBetween(0, 5000).WithMessage("De prijs moet een getal tussen 0 en 5000 zijn");
        RuleFor(x => x.Stock).NotEmpty().WithMessage("De voorraad mag niet leeg zijn")
          .InclusiveBetween(1, 1000).WithMessage("De voorraad moet een getal tussen 1 en 1000 zijn");
      }
    }
  }
}
