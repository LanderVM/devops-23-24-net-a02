
using Microsoft.AspNetCore.Mvc;
using shared.Equipment;
using shared.Formulas;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormulaController : ControllerBase
{
  private readonly ILogger<EquipmentController> _logger;
  private readonly IFormulaService _formulaService;

  public FormulaController(ILogger<EquipmentController> logger, IFormulaService formulaService)
  {
    _logger = logger;
    _formulaService = formulaService;
  }

  [HttpGet]
  [SwaggerOperation("Returns a list of formulas available.")]
  public async Task<FormulaResult.Index> GetFormulas() { 
    return await _formulaService.GetIndexAsync();
  }
  
  [SwaggerOperation("Edits a formula item in the catalog.")]
  [HttpPut("{formulaId}")]
  public async Task<FormulaResult.Edit> Edit(int formulaId, FormulaDto.Mutate model)
  {
    return await _formulaService.UpdateAsync(formulaId, model);
  }
  
}


