using System.Net.Mail;
using Api.Data;
using devops_23_24_net_a02.Shared.DTOs;
using Domain.Customers;

namespace Server.Services;

public class EmailService : IEmailService
{
  private readonly BlancheDbContext _dbContext;

  public EmailService(BlancheDbContext blancheDbContext)
  {
    _dbContext = blancheDbContext;
  }


  public async Task<int> CreateAsync(EmailDto.CreateEmail model)
  {
    var correspondingEmail = _dbContext.Emails.FirstOrDefault(existingMail => existingMail.Value.Equals(model.Email));
    if (correspondingEmail is not null)
      return correspondingEmail.Id;

    Email email = new Email(model.Email);
    _dbContext.Emails.Add(email);
    await _dbContext.SaveChangesAsync();
    return email.Id;
  }
}
