namespace devops_23_24_net_a02.Client;

public class FormulaState
{
  public int ChosenFormulaId { get; set; }

  public string Title { get; set; }

  public List<string> Attributes { get; set; }

  public decimal PricePerDayExtra { get; set; }

  public List<decimal> BasePrice { get; set; }

  public void Clear()
  {
    ChosenFormulaId = 0;
    Title = null;
    Attributes = null;
    PricePerDayExtra = 0;
    BasePrice = null;
  }
}
