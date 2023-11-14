using Api.Data;
using Microsoft.EntityFrameworkCore;
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
}

