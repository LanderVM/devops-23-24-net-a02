﻿using Domain.Quotations;
using FluentValidation;
using Shared.Common;
using Shared.Customer;
using Shared.Customer;
using shared.Equipment;

namespace shared.Quotations;

public static class QuotationDto
{

  public class Index
  {
    public int QuotationId { get; set; } 
    public CustomerDto.Index Customer { get; set; }
    public String CreatedAt { get; set; }
    
  }
  
  public class Dates { 
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }
  public class Create
  {
    public int FormulaId { get; set; }
    public AddressDto EventLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public List<EquipmentDto.Lines> Equipments { get; set; }
    public CustomerDto.Create Customer { get; set; }
    public bool IsTripelBier { get; set; }
    
    public int NumberOfPeople { get; set; }
  }

  public class Validator : AbstractValidator<Create>
  {
    public Validator()
    {
      RuleFor(model => model.FormulaId).NotEmpty();
      RuleFor(model => model.FormulaId).Must(id => id >= 1).WithMessage("Formula id must be a positive id!");
      RuleFor(model => model.EventLocation).NotEmpty();
      RuleFor(model => model.StartTime).NotEmpty();
      RuleFor(model => model.EndTime).NotEmpty();
     RuleFor(model => new { model.StartTime, model.EndTime })
        .Must(model => (model.EndTime - model.StartTime).TotalSeconds > 0)
        .WithMessage("End time cannot be before start time!");
     RuleFor(model => model.Customer).NotEmpty();
     //RuleFor(model => model.Customer).SetValidator(new CustomerDto.Validator());
     //RuleFor(model => model.IsTripelBier).NotEmpty(); TODO
    }
  }
}
