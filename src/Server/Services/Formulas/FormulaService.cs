using Api.Data;
using devops_23_24_net_a02.Client.Pages.Home;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using shared.Equipment;
using shared.Formulas;

namespace server.Services;
public class FormulaService: IFormulaService
{

  private readonly BlancheDbContext _dbContext;

  public FormulaService(BlancheDbContext blancheDbContext)
  {
    _dbContext = blancheDbContext;
  }

  public async Task<FormulaResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Formulas.AsQueryable();

    int totalAmount = await query.CountAsync();

      IEnumerable<FormulaDto.Index> items = await query.OrderBy(x => x.Id).Select(
        x=> new FormulaDto.Index { 
          Id = x.Id,
          Title = x.Description.Title,
          Attributes = x.Description.Attributes,
          PricePerDayExtra = x.PricePerDayExtra,
          BasePrice = x.BasePrice,
      }
      ).ToListAsync();

    var result = new FormulaResult.Index
    {
      Formulas = items,
      TotalAmount = totalAmount,
    };

    return result;
  }

  public async Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model)
  {
    
    Formula? formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
    {
      throw new Exception($"Equipment with id: {formulaId} doesn't exists");
    }



    formula.Description = new Description(model.Title, model.Attributes);
    formula.BasePrice = model.BasePrice;
    formula.PricePerDayExtra = model.PricePerDayExtra;
    
    
    
    await _dbContext.SaveChangesAsync();

    FormulaResult.Edit result = new FormulaResult.Edit
    {
      Id = formula.Id
    };

    return result;
  }
}

