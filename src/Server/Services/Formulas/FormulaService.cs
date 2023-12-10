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

  public async Task<FormulaDto.Mutate> GetSpecificMutateAsync(int formulaId)
  {
    Formula? formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
      throw new Exception($"formula with id: {formulaId} not found");
    
    string attributes = string.Join("\n", formula.Description.Attributes);
    string basePrice = string.Join("\n", formula.BasePrice);
    
    FormulaDto.Mutate mutate = new FormulaDto.Mutate{ 
      Title = formula.Description.Title,
      Attributes = attributes,
      PricePerDayExtra = formula.PricePerDayExtra,
      BasePrice= basePrice,
    };

    return mutate;
  }

  public async Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model)
  {
    
    Formula? formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
    {
      throw new Exception($"Equipment with id: {formulaId} doesn't exists");
    }



    formula.Description = new Description(model.Title, model.Attributes.Split('\n').ToList());
    formula.BasePrice = model.BasePrice.Split('\n').Select(decimal.Parse).ToList();;
    formula.PricePerDayExtra = model.PricePerDayExtra;
    
    
    
    await _dbContext.SaveChangesAsync();

    FormulaResult.Edit result = new FormulaResult.Edit
    {
      Id = formula.Id
    };

    return result;
  }
}

