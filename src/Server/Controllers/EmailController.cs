using devops_23_24_net_a02.Shared.Emails;
using Microsoft.AspNetCore.Mvc;
using Shared.Emails;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

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
  [SwaggerOperation("Adds a user to the subscribed email addresses list and sends them an email containing more information about the food truck")]
  public async Task<IActionResult> RegisterEmail(EmailDto.Create model)
  {
    _logger.Log(LogLevel.Information, "Registering new email {model.Email}", model.Email);
    int emailIid = await _emailService.CreateAsync(model);
    _logger.Log(LogLevel.Information, "Registered email {model.Email}", model.Email);
    return CreatedAtAction(nameof(RegisterEmail), new EmailResponse.Create { EmailId = emailIid});
  }

  [HttpGet]
  [Authorize]
  [SwaggerOperation("Get a list of subscribed email addresses")]
  public async Task<EmailResult.Index> GetEmail()
  {
    _logger.Log(LogLevel.Information, "Returning list of subscribed email address");
    return await _emailService.GetEmailsAsync();
  }
}
