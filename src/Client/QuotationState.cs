using MudBlazor;
using shared.Formulas;

namespace devops_23_24_net_a02.Client;

public class QuotationState
{
  public FormulaDto.Index Formula { get; set; }
  public DateRange DateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public int EstimatedNumberPeople { get; set; }
  public bool IsTripelBier { get; set; }

  public bool WithExtraMaterial { get; set; } = false;
}

