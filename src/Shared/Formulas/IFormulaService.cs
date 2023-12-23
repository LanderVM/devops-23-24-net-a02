namespace shared.Formulas;

public interface IFormulaService
{
  public Task<FormulaResult.Index> GetIndexAsync();
  
  public Task<FormulaDto.Mutate> GetSpecificMutateAsync(int formulaId);
  
  public Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model);
  
  public Task<FormulaResult.EditWithoutImage> UpdateWithoutImageAsync(int formulaId, FormulaDto.Mutate model);
}

