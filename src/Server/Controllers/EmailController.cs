using devops_23_24_net_a02.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
  private readonly ILogger<EmailController> _logger;
  private readonly IEmailService _emailService;

  public EmailController(ILogger<EmailController> logger, IEmailService emailService)
  {
    _logger = logger;
    _emailService = emailService;
  }

  [HttpPost]
  [SwaggerOperation("Saves and sends an email with more information to the provided email address")]
  public async Task<IActionResult> RegisterEmail(EmailDto.CreateEmail model)
  {
    _logger.Log(LogLevel.Information, "Registering new email {model.Email}", model.Email);
    int emailIid = await _emailService.CreateAsync(model);
    _logger.Log(LogLevel.Information, "Registered email {model.Email}", model.Email);
    return CreatedAtAction(nameof(RegisterEmail), new {id = emailIid});
  }
}
