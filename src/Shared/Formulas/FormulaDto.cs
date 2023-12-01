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
}

