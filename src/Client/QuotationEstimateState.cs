using System.Collections.Generic;
using FluentValidation;
using MudBlazor;
using shared.Equipment;
using shared.Formulas;
using Shared.Common;

namespace devops_23_24_net_a02.Client;

public class QuotationEstimateState
{
  public QuotationEstimateClass QuotationEstimateObject = new();
  public QuotationEstimateClassValidator quotationEstimateClassValidator = new();

  public class QuotationEstimateClass
  {
    public int FormulaId { get; set; } = 1;
    public IEnumerable<EquipmentDto.Select>? Equipment { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EstimatedNumberOfPeople { get; set; } = 1;
    public bool IsTripelBier { get; set; }
    public DateRange DateRange = new DateRange();
  }

  public class QuotationEstimateClassValidator : AbstractValidator<QuotationEstimateClass>
  {
    public QuotationEstimateClassValidator()
    {
      RuleFor(x => x.FormulaId).NotEmpty();
      RuleFor(x => x.StartTime).NotEmpty();
      RuleFor(x => x.EndTime).NotEmpty();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
      var result = await ValidateAsync(ValidationContext<QuotationEstimateClass>.CreateWithOptions((QuotationEstimateClass)model, x => x.IncludeProperties(propertyName)));
      if (result.IsValid)
        return Array.Empty<string>();
      return result.Errors.Select(e => e.ErrorMessage);
    };
  }

}

