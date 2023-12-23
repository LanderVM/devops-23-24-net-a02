using Api.Data;
using devops_23_24_net_a02.Domain.Files;
using devops_23_24_net_a02.Services.Files;
using Domain.Formulas;
using Microsoft.EntityFrameworkCore;
using shared.Formulas;

namespace server.Services;

public class FormulaService : IFormulaService
{
  private readonly BlancheDbContext _dbContext;
  private readonly IStorageService _storageService;

  public FormulaService(BlancheDbContext blancheDbContext, IStorageService storageService)
  {
    _dbContext = blancheDbContext;
    _storageService = storageService;
  }

  public async Task<FormulaResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Formulas.AsQueryable();

    var totalAmount = await query.CountAsync();

    IEnumerable<FormulaDto.Index> items = await query.OrderBy(x => x.Id).Select(
      x => new FormulaDto.Index
      {
        Id = x.Id,
        Title = x.Description.Title,
        Attributes = x.Description.Attributes,
        PricePerDayExtra = x.PricePerDayExtra,
        BasePrice = x.BasePrice,
        IsActive = x.IsActive,
        ImageUrl = x.ImageUrl
      }
    ).ToListAsync();

    var result = new FormulaResult.Index { Formulas = items, TotalAmount = totalAmount };

    return result;
  }

  public async Task<FormulaDto.Mutate> GetSpecificMutateAsync(int formulaId)
  {
    var formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
    {
      throw new Exception($"formula with id: {formulaId} not found");
    }


    var attributes = string.Join("\n", formula.Description.Attributes);
    var basePrice = string.Join("\n", formula.BasePrice);

    var mutate = new FormulaDto.Mutate
    {
      Title = formula.Description.Title,
      Attributes = attributes,
      PricePerDayExtra = formula.PricePerDayExtra,
      BasePrice = basePrice,
      IsActive = formula.IsActive,
      ImageData = new FormulaDto.ImageData
      {
        ImageUrl = "https://via.placeholder.com/350x300", AltText = "placeholder txt"
      }
    };

    return mutate;
  }

  public async Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model)
  {
    var formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
    {
      throw new Exception($"Formula with id: {formulaId} doesn't exist");
    }

    var image = new Image(_storageService.BasePath, model.ImageContentType!);


    formula.Description = new Description(model.Title, model.Attributes.Split('\n').ToList());
    formula.BasePrice = model.BasePrice.Split('\n').Select(decimal.Parse).ToList();
    formula.PricePerDayExtra = model.PricePerDayExtra;
    formula.IsActive = model.IsActive;
    formula.ImageUrl = image.FileUri.ToString();

    await _dbContext.SaveChangesAsync();

    var uploadSas = _storageService.GenerateImageUploadSas(image);

    var result = new FormulaResult.Edit
    {
      Id = formula.Id, Image = new ImageData { ImageUrl = uploadSas.ToString(), AltText = formula.Description.Title }
    };

    return result;
  }


  public async Task<FormulaResult.EditWithoutImage> UpdateWithoutImageAsync(int formulaId, FormulaDto.Mutate model)
  {
    var formula = await _dbContext.Formulas.FirstOrDefaultAsync(x => x.Id == formulaId);

    if (formula is null)
    {
      throw new Exception($"Formula with id: {formulaId} doesn't exist");
    }

    formula.Description = new Description(model.Title, model.Attributes.Split('\n').ToList());
    formula.BasePrice = model.BasePrice.Split('\n').Select(decimal.Parse).ToList();
    formula.PricePerDayExtra = model.PricePerDayExtra;
    formula.IsActive = model.IsActive;

    await _dbContext.SaveChangesAsync();


    var result = new FormulaResult.EditWithoutImage { Id = formula.Id };

    return result;
  }
}
