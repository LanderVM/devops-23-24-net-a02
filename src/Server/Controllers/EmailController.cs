﻿using devops_23_24_net_a02.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("[controller]")]
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
  public async Task<IActionResult> RegisterEmail(EmailDto.CreateEmail model)
  {
    _logger.Log(LogLevel.Information, "Registering new email {model.Email}", model.Email);
    int emailIid = await _emailService.CreateAsync(model);
    _logger.Log(LogLevel.Information, "Registered email {model.Email}", model.Email);
    return CreatedAtAction(nameof(RegisterEmail), new {id = emailIid});
  }
}
