using Api.Data;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using shared.Equipment;
using devops_23_24_net_a02.Services.Files;
using devops_23_24_net_a02.Domain.Files;

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
    Equipment? e = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Description.Title.Equals(model.Title));

    if (e is not null)
      throw new Exception($"equipment with title: {model.Title} already exists");

    Image image = new Image(_storageService.BasePath, model.ImageContentType!);
    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list)
    {
      string s2 = s.Trim();
      attributes.Add(s2);
    }

    Equipment equipment = new Equipment(model.Title, attributes, model.Price, model.Stock, image.FileUri.ToString());

    _dbContext.Equipments.Add(equipment);
    await _dbContext.SaveChangesAsync();

    Uri uploadSas = _storageService.GenerateImageUploadSas(image);

    EquipmentResult.CreateWithImage result = new EquipmentResult.CreateWithImage
    {
      Image = new ImageData
      {
        ImageUrl = uploadSas.ToString(),
        AltText = equipment.Description.Title
      },
      Id = equipment.Id
    };

    return result;
  }

  public async Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model)
  {
    Equipment? e = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Description.Title.Equals(model.Title));

    if (e is not null)
      throw new Exception($"equipment with title: {model.Title} already exists");

    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list) { 
      string s2 = s.Trim();
      attributes.Add(s2);
    }

    Equipment equipment = new Equipment(model.Title, attributes, model.Price, model.Stock);

    _dbContext.Equipments.Add(equipment);
    await _dbContext.SaveChangesAsync();

    EquipmentResult.Create result = new EquipmentResult.Create
    {
      Id = equipment.Id
    };

    return result;
  }

  public async Task<int> DeleteAsync(int equipmentId)
  {
    Equipment? equipment = await _dbContext.Equipments.SingleOrDefaultAsync(x => x.Id==equipmentId);

    if (equipment is null) {
      throw new Exception($"Equipment with id: {equipmentId} doesnt exist");
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

    string noImageUrl = "https://via.placeholder.com/350x300";

    var items = await query
       .OrderBy(x => x.Id)
       .Select(x => new EquipmentDto.Index
       {
         Id = x.Id,
         Title = x.Description.Title,
         Attributes = x.Description.Attributes,
         Price = x.Price,
         Stock = x.Stock,
         ImageData = new ImageData { 
           ImageUrl = x.ImageUrl,
           AltText = x.Description.Title,
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
      ImageData = new ImageData
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

  public async Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId)
  {
    Equipment? equipment = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (equipment is null)
      throw new Exception($"equipment with id: {equipmentId} not found");

    string attributes = string.Join(";", equipment.Description.Attributes);

    EquipmentDto.Mutate mutate = new EquipmentDto.Mutate{ 
      Title = equipment.Description.Title,
      Attributes = attributes,
      Price = equipment.Price,
      Stock= equipment.Stock,
      ImageData = new ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300",
        AltText = "placeholder txt",
      },
    };

    return mutate;
  }

  public async Task<EquipmentResult.CreateWithImage> UpdateWithImageAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    Equipment? eq = await _dbContext.Equipments.FirstOrDefaultAsync(x=>x.Id == equipmentId);

    if (eq is null)
    {
      throw new Exception($"Equipment with id: {equipmentId} doesn't exists");
    }

    Image image = new Image(_storageService.BasePath, model.ImageContentType!);

    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list)
    {
      string s2 = s.Trim();
      attributes.Add(s2);
    }

    Description des = new Description(model.Title, attributes);
    eq.Description = des;
    eq.Stock = model.Stock;
    eq.Price = model.Price;
    eq.ImageUrl = image.FileUri.ToString();

    await _dbContext.SaveChangesAsync();

    Uri uploadSas = _storageService.GenerateImageUploadSas(image);

    EquipmentResult.CreateWithImage result = new EquipmentResult.CreateWithImage
    {
      Image = new ImageData
      {
        ImageUrl = uploadSas.ToString(),
        AltText = eq.Description.Title
      },
      Id = eq.Id
    };

    return result;
  }

  public async Task<EquipmentResult.Create> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    Equipment? eq = await _dbContext.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId);

    if (eq is null)
    {
      throw new Exception($"Equipment with id: {equipmentId} doesn't exists");
    }

    List<string> list = model.Attributes.Split(';').ToList();
    List<string> attributes = new List<string>();

    foreach (string s in list)
    {
      string s2 = s.Trim();
      attributes.Add(s2);
    }

    Description des = new Description(model.Title, attributes);
    eq.Description = des;
    eq.Stock = model.Stock;
    eq.Price = model.Price;

    await _dbContext.SaveChangesAsync();

    EquipmentResult.Create result = new EquipmentResult.Create
    {
      Id = eq.Id
    };

    return result;
  }
}
