namespace shared.Formulas;

public interface IFormulaService
{
  public Task<FormulaResult.Index> GetIndexAsync();
}

