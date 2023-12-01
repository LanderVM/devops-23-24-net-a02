using Api.Data;
using devops_23_24_net_a02.Shared.Emails;
using Domain.Common;
using Domain.Customers;
using Microsoft.Extensions.Options;

namespace Server.Services;

public class EmailService : IEmailService
{
  private readonly BlancheDbContext _dbContext;
  private IOptions<EmailConfiguration> _emailConfiguration;

  public EmailService(BlancheDbContext blancheDbContext, IOptions<EmailConfiguration> emailConfiguration)
  {
    _dbContext = blancheDbContext;
    _emailConfiguration = emailConfiguration;
  }

  public async Task<int> CreateAsync(EmailDto.Create model)
  {
    var correspondingEmail = _dbContext.Emails.FirstOrDefault(existingMail => existingMail.Value.Equals(model.Email));
    if (correspondingEmail is not null)
      return correspondingEmail.Id;

    Email email = new Email(model.Email);
    _dbContext.Emails.Add(email);
    await _dbContext.SaveChangesAsync();

    // Retrieve email configuration values
    EmailConfiguration emailConfig = EmailConfiguration.GetInstance();
    string mail = emailConfig.Mail;
    string password = emailConfig.Password;

    MailSender mailSender = new MailSender(mail, email.Value, new System.Net.NetworkCredential(mail, password));
    //mailSender.SendNewQuote("Test");

    return email.Id;
  }
}
