using System.Collections.Generic;
using FluentValidation;
using GoogleMapsComponents.Maps.Places;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using shared.Equipment;
using shared.Formulas;
using Shared.Common;

namespace devops_23_24_net_a02.Client;

public class QuotationEstimateState
{
  public FormulaDto.Select? FormulaId { get; set; }
  public IEnumerable<EquipmentDto.Select>? Equipment { get; set; } = default!;
  public int EstimatedNumberOfPeople { get; set; } = 1;
  public bool IsTripelBier { get; set; } = false;
  public DateRange DateRange = new();

  public void Clear()
  {
    FormulaId = null;
    Equipment = null;
    EstimatedNumberOfPeople = 1;
    IsTripelBier = false;
    DateRange = new();
  }
}
