
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
    _logger.Log(LogLevel.Information, "Fetching list of formulas");
    var result = await _formulaService.GetIndexAsync();
    _logger.Log(LogLevel.Information, "Found {result.TotalAmount} formulas", result.TotalAmount);
    return result;
  }
  
  [HttpGet("{formulaId}")]
  [SwaggerOperation("Returns a specific formula based on id.")]
  public async Task<FormulaDto.Mutate> GetSpecificFormulaMutate(int formulaId)
  {
    _logger.Log(LogLevel.Information, "Fetching formula with id {formulaId}", formulaId);
    var result = await _formulaService.GetSpecificMutateAsync(formulaId);
    _logger.Log(LogLevel.Information, "Found formula with id {formulaId}: {result.Title}", formulaId, result.Title);
    return result;
  }
  
  [SwaggerOperation("Edits a formula in the catalog.")]
  [HttpPut("{formulaId}")]
  public async Task<FormulaResult.Edit> Edit(int formulaId, FormulaDto.Mutate model)
  {
    _logger.Log(LogLevel.Information, "Fetching formula with id {formulaId} to edit based off model: {model.ToString}", formulaId, model.ToString());
    var result = await _formulaService.UpdateAsync(formulaId, model);
    _logger.Log(LogLevel.Information, "Edited equipment with id {formulaId}: {result.ToString}", formulaId, result.ToString());
    return result;
  }
  
}


