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

  public Task<int> CreateAsync(EquipmentDto.Create model)
  {
    throw new NotImplementedException();
  }

  public Task<int> DeleteAsync(int equipmentId)
  {
    throw new NotImplementedException();
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
         Attributes = x.Description.Attributes,
         Price = x.Price,
         Stock = x.Stock,
         ImageData = new EquipmentDto.ImageData { 
           ImageUrl = "https://via.placeholder.com/350x300",
           AltText = "placeholder txt",
         },
         FormulaIds = x.Formulas.Select(x => x.Id).ToList(),
       }).ToListAsync();

    var result = new EquipmentResult.Index
    {
      Equipment = items,
      TotalAmount = totalAmount
    };
    return result;
  }

  public Task<int> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    throw new NotImplementedException();
  }
}
