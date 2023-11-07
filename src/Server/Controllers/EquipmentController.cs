using System.Runtime.InteropServices;
using devops_23_24_net_a02.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using Server.Services;
using shared.Equipment;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipmentController : ControllerBase
{
  private readonly ILogger<EquipmentController> _logger;
  private readonly IEquipmentService _equipmentService;

  public EquipmentController(ILogger<EquipmentController> logger, IEquipmentService equipmentService)
  {
    _logger = logger;
    _equipmentService = equipmentService;
  }


  [SwaggerOperation("Returns a list of equipment available in the extra's catalog.")]
  [HttpGet]
  public async Task<EquipmentResult.Index> GetEquipment()
  {
    return await _equipmentService.GetIndexAsync();
  }
}

