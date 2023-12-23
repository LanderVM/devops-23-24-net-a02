using MudBlazor;
using shared.Equipment;
using shared.Formulas;

namespace devops_23_24_net_a02.Client;

public class QuotationEstimateState
{
  public DateRange DateRange = new();
  public FormulaDto.Select? FormulaId { get; set; }
  public IEnumerable<EquipmentDto.Select>? Equipment { get; set; }
  public int EstimatedNumberOfPeople { get; set; } = 1;
  public bool IsTripelBier { get; set; }

  public void Clear()
  {
    FormulaId = null;
    Equipment = null;
    EstimatedNumberOfPeople = 1;
    IsTripelBier = false;
    DateRange = new DateRange();
  }
}
