using Api.Data;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using shared.Equipment;
using Domain.Exceptions;

namespace server.Services;
public class EquipmentService : IEquipmentService
{

  private readonly BlancheDbContext _dbContext;

  public EquipmentService(BlancheDbContext blancheDbContext)
  {
    _dbContext = blancheDbContext;
  }

  public async Task<int> CreateAsync(EquipmentDto.Create model)
  {
    Equipment? e = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Description.Title.Equals(model.Title));

    if (e is not null)
      throw new EntityAlreadyExistsException(nameof(Equipment),nameof(Equipment.Description.Title),model.Title);

    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list) { 
      string s2 = s.Trim();
      attributes.Add(s2);
    }
    
    Equipment equipment = new Equipment(model.Title,attributes,model.Price,model.Stock);
    equipment.IsActive = model.IsActive;

    _dbContext.Equipments.Add(equipment);

    await _dbContext.SaveChangesAsync();

    return equipment.Id;
  }

  public async Task<int> DeleteAsync(int equipmentId)
  {
    Equipment? equipment = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Id==equipmentId);

    if (equipment is null) {
      throw new EntityNotFoundException(nameof(Equipment),equipmentId);
    }

    int id = equipment.Id;

    _dbContext.Equipments.Remove(equipment);
    await _dbContext.SaveChangesAsync();

    return id;
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
         IsActive = x.IsActive,
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
      IsActive = x.IsActive,
      ImageData = new EquipmentDto.ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300",
        AltText = "placeholder txt",
      },
      FormulaIds = x.Formulas.Select(x => x.Id).ToList(),
    }).FirstOrDefaultAsync(x => x.Id == equipmentId);

    if(equipment == null)
    {
      throw new EntityNotFoundException(nameof(Equipment),equipmentId);
    }

    return equipment;
  }

  public async Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId)
  {
    Equipment? equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
      throw new EntityNotFoundException(nameof(Equipment),equipmentId);

    string attributes = string.Join(";", equipment.Description.Attributes);

    EquipmentDto.Mutate mutate = new EquipmentDto.Mutate{ 
      Title = equipment.Description.Title,
      Attributes = attributes,
      Price = equipment.Price,
      Stock= equipment.Stock,
      IsActive = equipment.IsActive,
      ImageData = new EquipmentDto.ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300",
        AltText = "placeholder txt",
      },
    };

    return mutate;
  }

  public async Task UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    Equipment? equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x=>x.Id == equipmentId);

    if (equipment is null)
    {
      throw new EntityNotFoundException(nameof(Equipment),equipmentId);
    }

    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list)
    {
      string s2 = s.Trim();
      attributes.Add(s2);
    }

    Description description = new Description(model.Title, attributes);
    equipment.Description = description;
    equipment.Stock = model.Stock;
    equipment.Price = model.Price;
    equipment.IsActive = model.IsActive;

    await _dbContext.SaveChangesAsync();
  }
}
