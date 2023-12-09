using Api.Data;
using devops_23_24_net_a02.Shared.Emails;
using Domain.Common;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
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

  public async Task<EmailResult.Index> GetEmailsAsync()
  {
    var query = _dbContext.Emails.AsQueryable();

    int totalAmount = await query.CountAsync();

    IEnumerable<EmailDto.Index> emailList = await query.Select(x => new EmailDto.Index
    {
      EmailAddress = x.Value
    }).ToListAsync();

    var result = new EmailResult.Index
    {
      EmailAddresses = emailList,
      TotalAmount = totalAmount
    };

    return result;

  }
}
