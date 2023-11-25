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
    return await _equipmentService.GetIndexAsync();
  }

  [HttpGet("{equipmentId}")]
  [SwaggerOperation("Returns a specific equipment based on id.")]
  public async Task<EquipmentDto.Index> GetSpecificEquipment(int equipmentId)
  {
    return await _equipmentService.GetSpecificIndexAsync(equipmentId);
  }
}

