namespace shared.Formulas;

public interface IFormulaService
{
  public Task<FormulaResult.Index> GetIndexAsync();
  
  public Task<FormulaResult.Edit> UpdateAsync(int equipmentId, FormulaDto.Mutate model);
}

