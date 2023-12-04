﻿using FluentValidation;

namespace shared.Equipment;

public static class EquipmentDto
{
  public class Index { 
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
    public bool IsActive { get; set; }
    
    public class Validator : AbstractValidator<EquipmentDto.Lines>
    {
      public Validator()
      {
        RuleFor(x => x.Amount).NotEmpty();
      }

    }
  }
  public class ImageData {
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
  public class Create {
    public string Title { get; set; }
    public string Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ImageData ImageData { get; set; }

    public bool IsActive { get; set; }



    public class Validator : AbstractValidator<EquipmentDto.Create>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Attributes).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).NotEmpty().InclusiveBetween(0, 5000);
        RuleFor(x => x.Stock).NotEmpty().InclusiveBetween(1, 1000);
      }

    }


  }

  public class Mutate {
    public string Title { get; set; }
    public string Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ImageData ImageData { get; set; }

    public bool IsActive { get; set; }

    public class Validator : AbstractValidator<EquipmentDto.Mutate>
    {
      public Validator()
      {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Attributes).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).NotEmpty().InclusiveBetween(0, 5000);
        RuleFor(x => x.Stock).NotEmpty().InclusiveBetween(1, 1000);
        //RuleFor(x => x.IsActive).NotEmpty();
      }

    }
  }
}
