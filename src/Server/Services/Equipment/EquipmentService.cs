using Api.Data;
using Domain.Formulas;
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

  public async Task<EquipmentDto.Index> GetSpecificIndexAsync(int equipmentId)
  {
    EquipmentDto.Index? equipment = await _dbContext.Equipments.Select(x => new EquipmentDto.Index
    {
      Id = x.Id,
      Title = x.Description.Title,
      Attributes = x.Description.Attributes,
      Price = x.Price,
      Stock = x.Stock,
      ImageData = new EquipmentDto.ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300",
        AltText = "placeholder txt",
      },
      FormulaIds = x.Formulas.Select(x => x.Id).ToList(),
    }).FirstOrDefaultAsync(x => x.Id == equipmentId);

    if(equipment == null)
    {
      throw new Exception($"equipment with id: {equipmentId} not found");
    }

    return equipment;
  }

  public async Task UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    Equipment? equipment = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
      throw new Exception($"equipment with id: {equipmentId} not found");

    Description description = new(model.Title, model.Attributes);

    equipment.UpdatedAt = DateTime.UtcNow;
    equipment.Description = description;
    equipment.Price = model.Price;
    equipment.Stock = model.Stock;

    await _dbContext.SaveChangesAsync();
  }
}
