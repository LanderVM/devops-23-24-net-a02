using Api.Data;
using devops_23_24_net_a02.Shared.Emails;
using Domain.Common;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.Exceptions;
using Domain.Formulas;
using Domain.Quotations;
using shared.Quotations;

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
    Email? correspondingEmail = _dbContext.Emails.FirstOrDefault(existingMail => existingMail.Value.Equals(model.Email));
    if (correspondingEmail is not null)
      throw new EntityAlreadyExistsException(nameof(Email),nameof(Email.Value),model.Email);

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

  public async Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model)
  {
    QuotationResponse.Edit result = new QuotationResponse.Edit
    {
      QuotationId = model.QuotationId
    };

    if (model.Status != QuotationStatus.Accepted)
    {

      return result;
    }

    var formule = _dbContext.Formulas.FirstOrDefault(formule => formule.Id == model.FormulaId);
    if (formule is null)
    {
      throw new Exception($"Formula with Id: {model.FormulaId}, does not exist");
    }

    var equipmentItems = new List<QuotationLine>();
    foreach (var item in model.Equipments)
    {
      var equipment = _dbContext.Equipments.FirstOrDefault(equipment => equipment.Id == item.EquipmentId);

      equipmentItems.Add(new QuotationLine(new Equipment(equipment.Description.Title, equipment.Price), item.Amount));
    }

    Customer customer = new(
      model.Customer.FirstName,
      model.Customer.LastName,
      new Email(model.Customer.Email.Email),
      new BillingAddress(model.Customer.BillingAddress.Street, model.Customer.BillingAddress.HouseNumber, model.Customer.BillingAddress.City, model.Customer.BillingAddress.PostalCode),
      new PhoneNumber(model.Customer.PhoneNumber),
      model.Customer.VatNumber
      );

    EventLocation eventLocation = new(
      model.EventLocation.Street,
      model.EventLocation.HouseNumber,
      model.EventLocation.City,
      model.EventLocation.PostalCode
      );

    EmailConfiguration emailConfig = EmailConfiguration.GetInstance();
    string mail = emailConfig.Mail;
    string password = emailConfig.Password;

    MailSender mailSender = new MailSender(mail, model.Customer.Email.Email, new System.Net.NetworkCredential(mail, password));

    Quotation quotation = new(formule, customer, eventLocation, equipmentItems, model.StartTime, model.EndTime, model.NumberOfPeople, model.IsTripelBier)
    {
      Opmerking = model.Opmerking,
      QuotationLines = equipmentItems
    };

    Console.WriteLine(model.Opmerking);
    await Console.Out.WriteLineAsync(quotation.Opmerking);

    mailSender.SendNewQuote(quotation);

    return result;
  }

}
