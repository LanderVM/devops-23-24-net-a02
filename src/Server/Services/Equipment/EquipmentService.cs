using Api.Data;
using Microsoft.EntityFrameworkCore;
using shared.Equipment;

namespace server.Services;
public class EquipmentService : IEquipmentService
{

  private readonly BlancheDbContext _dbContext;

  public EquipmentService(BlancheDbContext blancheDbContext)
  {
    _dbContext = blancheDbContext;
  }
  public async Task<EquipmentResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Equipments.AsQueryable();

    int totalAmount = await query.CountAsync();

    var items = await query
       .OrderBy(x => x.Id)
       .Select(x => new EquipmentDto.Index
       {
         Id = x.Id,
         Title = x.Description.Title,
         Subtext = x.Description.Subtext,
         Price = x.Price,
         Stock = x.Stock,
         ImageData = new List<string> { x.Image.ImageUrl,x.Image.AltText},
         FormulaIds = x.Formulas.Select(x => x.Id).ToList(),
       }).ToListAsync();

    var result = new EquipmentResult.Index
    {
      Equipment = items,
      TotalAmount = totalAmount
    };
    return result;
  }
}
