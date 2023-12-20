using Microsoft.AspNetCore.Mvc;
using shared.Equipment;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
  private readonly ILogger<EquipmentController> _logger;
  private readonly IEquipmentService _equipmentService;

  public EquipmentController(ILogger<EquipmentController> logger, IEquipmentService equipmentService)
  {
    _logger = logger;
    _equipmentService = equipmentService;
  }


  [HttpGet]
  [SwaggerOperation("Returns a list of equipment available in the extra's catalog.")]
  public async Task<EquipmentResult.Index> GetEquipment()
  {
    _logger.Log(LogLevel.Information, "Returning equipment list");
    return await _equipmentService.GetIndexAsync();
  }

  [HttpGet("/api/Equipment/Active")]
  [SwaggerOperation("Returns a list of equipment available in the extra's catalog that is active.")]
  public async Task<EquipmentResult.ActiveEquipment> GetActiveEquipment()
  {
    _logger.Log(LogLevel.Information, "Returning active equipment list");
    return await _equipmentService.GetActiveEquipmentAsync();
  }

  [HttpGet("{equipmentId}")]
  [SwaggerOperation("Returns a specific equipment based on id.")]
  public async Task<EquipmentDto.Mutate> GetSpecificEquipmentMutate(int equipmentId)
  {
    _logger.Log(LogLevel.Information, "Fetching equipment with id {equipmentId}", equipmentId);
    var result = await _equipmentService.GetSpecificMutateAsync(equipmentId);
    _logger.Log(LogLevel.Information, "Found equipment with id {equipmentId}: {result.Title}", equipmentId, result.Title);
    return result;
  }

  [SwaggerOperation("Edits an equipment item in the catalog.")]
  [HttpPut("{equipmentId}")]
  public async Task<EquipmentResult.Create> Edit(int equipmentId, EquipmentDto.Mutate model)
  {
    _logger.Log(LogLevel.Information, "Fetching equipment with id {equipmentId} to edit based off model: {model.ToString}", equipmentId, model.ToString());
    var result = await _equipmentService.UpdateAsync(equipmentId, model);
    _logger.Log(LogLevel.Information, "Edited equipment with id {equipmentId}: {result.ToString}", equipmentId, result.ToString());
    return result;
  }

  [SwaggerOperation("Edites an equipment item with an image in the catalog.")]
  [HttpPut("WithImage/{equipmentId}")]
  public async Task<EquipmentResult.CreateWithImage> EditWithImage(int equipmentId, EquipmentDto.Mutate model)
  {
    _logger.Log(LogLevel.Information, "Fetching equipment with id {equipmentId} to edit with images", equipmentId);
    var result = await _equipmentService.UpdateWithImageAsync(equipmentId, model);
    _logger.Log(LogLevel.Information, "Edited equipment with images with id {equipmentId}: {result.ToString}", equipmentId, result.ToString());
    return result;
  }

  [SwaggerOperation("Deletes equipment item from catalog.")]
  [HttpDelete("{equipmentId}")]
  public async Task<int> Delete(int equipmentId)
  {
    _logger.Log(LogLevel.Information, "Fetching equipment with id {equipmentId} to delete", equipmentId);
    int id = await _equipmentService.DeleteAsync(equipmentId);
    _logger.Log(LogLevel.Information, "Deleted equipment with id {equipmentId}", equipmentId);
    return id;
  }

  [SwaggerOperation("Creates equipment and adds it to the catalog.")]
  [HttpPost]
  public async Task<EquipmentResult.Create> Create(EquipmentDto.Create model)
  {
    _logger.Log(LogLevel.Information, "Creating new equipment based off model: {model.ToString()}", model.ToString());
    EquipmentResult.Create item = await _equipmentService.CreateAsync(model);
    _logger.Log(LogLevel.Information, "Created new equipment with id {item.Id}", item.Id);
    return item;
  }

  [HttpPost("WithImage")]
  [SwaggerOperation("Creates equipment with a image and adds it to the catalog.")]
  public async Task<EquipmentResult.CreateWithImage> CreateWithImage(EquipmentDto.Create model)
  {
    _logger.Log(LogLevel.Information, "Creating new equipment with image based off model: {model.ToString()}", model.ToString());
    EquipmentResult.CreateWithImage item = await _equipmentService.CreateWithImageAsync(model);
    _logger.Log(LogLevel.Information, "Created new equipment with id {item.Id}", item.Id);
    return item;
  }
}

