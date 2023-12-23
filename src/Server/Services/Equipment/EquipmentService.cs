using Api.Data;
using devops_23_24_net_a02.Domain.Files;
using devops_23_24_net_a02.Services.Files;
using Domain.Exceptions;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using shared.Equipment;

namespace server.Services;

public class EquipmentService : IEquipmentService
{
  private readonly BlancheDbContext _dbContext;
  private readonly IStorageService _storageService;

  public EquipmentService(BlancheDbContext blancheDbContext, IStorageService storageService)
  {
    _dbContext = blancheDbContext;
    _storageService = storageService;
  }

  public async Task<EquipmentResult.CreateWithImage> CreateWithImageAsync(EquipmentDto.Create model)
  {
    var e = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Description.Title.Equals(model.Title));

    if (e is not null)
    {
      throw new Exception($"equipment with title: {model.Title} already exists");
    }

    var image = new Image(_storageService.BasePath, model.ImageContentType!);
    var list = model.Attributes.Split(';').ToList();
    var attributes = new List<string>();

    foreach (var s in list)
    {
      var s2 = s.Trim();
      attributes.Add(s2);
    }

    var equipment = new Equipment(model.Title, attributes, model.Price, model.Stock, image.FileUri.ToString());

    _dbContext.Equipments.Add(equipment);
    await _dbContext.SaveChangesAsync();

    var uploadSas = _storageService.GenerateImageUploadSas(image);

    var result = new EquipmentResult.CreateWithImage
    {
      Image = new ImageData { ImageUrl = uploadSas.ToString(), AltText = equipment.Description.Title },
      Id = equipment.Id
    };

    return result;
  }

  public async Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model)
  {
    var e = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Description.Title.Equals(model.Title));

    if (e is not null)
    {
      throw new EntityAlreadyExistsException(nameof(Equipment), nameof(Equipment.Description.Title), model.Title);
    }

    var list = model.Attributes.Split(';').ToList();
    var attributes = new List<string>();

    foreach (var s in list)
    {
      var s2 = s.Trim();
      attributes.Add(s2);
    }

    var equipment = new Equipment(model.Title, attributes, model.Price, model.Stock);
    equipment.IsActive = model.IsActive;

    _dbContext.Equipments.Add(equipment);
    await _dbContext.SaveChangesAsync();

    var result = new EquipmentResult.Create { Id = equipment.Id };

    return result;
  }

  public async Task<int> DeleteAsync(int equipmentId)
  {
    var equipment = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
    {
      throw new EntityNotFoundException(nameof(Equipment), equipmentId);
    }

    var id = equipment.Id;

    _dbContext.Equipments.Remove(equipment);
    await _dbContext.SaveChangesAsync();

    return id;
  }

  public async Task<EquipmentResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Equipments.AsQueryable();

    var totalAmount = await query.CountAsync();

    var noImageUrl = "https://via.placeholder.com/350x300";

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
        ImageData = new EquipmentDto.ImageData { ImageUrl = x.ImageUrl, AltText = x.Description.Title },
        FormulaIds = x.Formulas.Select(x => x.Id).ToList()
      }).ToListAsync();

    var result = new EquipmentResult.Index { Equipment = items, TotalAmount = totalAmount };
    return result;
  }

  public async Task<EquipmentResult.ActiveEquipment> GetActiveEquipmentAsync()
  {
    var query = _dbContext.Equipments.AsQueryable().Where(x => x.IsActive == true);

    var totalAmount = await query.CountAsync();

    var noImageUrl = "https://via.placeholder.com/350x300";

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
        ImageData = new EquipmentDto.ImageData { ImageUrl = x.ImageUrl, AltText = x.Description.Title },
        FormulaIds = x.Formulas.Select(x => x.Id).ToList()
      }).ToListAsync();

    var result = new EquipmentResult.ActiveEquipment { Equipment = items, TotalAmount = totalAmount };
    return result;
  }


  public async Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId)
  {
    var equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
    {
      throw new EntityNotFoundException(nameof(Equipment), equipmentId);
    }

    var attributes = string.Join(";", equipment.Description.Attributes);

    var mutate = new EquipmentDto.Mutate
    {
      Title = equipment.Description.Title,
      Attributes = attributes,
      Price = equipment.Price,
      Stock = equipment.Stock,
      IsActive = equipment.IsActive,
      ImageData = new EquipmentDto.ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300", AltText = "placeholder txt"
      }
    };

    return mutate;
  }

  public async Task<EquipmentResult.CreateWithImage> UpdateWithImageAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
    {
      throw new EntityNotFoundException(nameof(Equipment), equipmentId);
    }

    var image = new Image(_storageService.BasePath, model.ImageContentType!);

    var list = model.Attributes.Split(';').ToList();
    var attributes = new List<string>();

    foreach (var s in list)
    {
      var s2 = s.Trim();
      attributes.Add(s2);
    }

    var description = new Description(model.Title, attributes);
    equipment.Description = description;
    equipment.Stock = model.Stock;
    equipment.Price = model.Price;
    equipment.IsActive = model.IsActive;
    equipment.ImageUrl = image.FileUri.ToString();

    await _dbContext.SaveChangesAsync();

    var uploadSas = _storageService.GenerateImageUploadSas(image);

    var result = new EquipmentResult.CreateWithImage
    {
      Image = new ImageData { ImageUrl = uploadSas.ToString(), AltText = equipment.Description.Title },
      Id = equipment.Id
    };

    return result;
  }

  public async Task<EquipmentResult.Create> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
    {
      throw new Exception($"Equipment with id: {equipmentId} doesn't exists");
    }

    var list = model.Attributes.Split(';').ToList();
    var attributes = new List<string>();

    foreach (var s in list)
    {
      var s2 = s.Trim();
      attributes.Add(s2);
    }

    var description = new Description(model.Title, attributes);
    equipment.Description = description;
    equipment.Stock = model.Stock;
    equipment.Price = model.Price;
    equipment.IsActive = model.IsActive;

    await _dbContext.SaveChangesAsync();

    var result = new EquipmentResult.Create { Id = equipment.Id };

    return result;
  }
}
