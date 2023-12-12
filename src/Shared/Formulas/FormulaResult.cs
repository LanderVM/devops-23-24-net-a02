namespace shared.Formulas;

public class FormulaResult
{
  public class Index { 
    
    public IEnumerable<FormulaDto.Index>? Formulas { get; set; }

    public int TotalAmount { get; set; }
  }
  
  public class Edit
  {
    public int Id { get; set; }
  }
  
  
}

