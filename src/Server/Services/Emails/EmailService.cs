﻿using System.Diagnostics;
using Api.Data;
using devops_23_24_net_a02.Shared.Emails;
using Domain.Common;
using Domain.Customers;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.Extensions.Options;
using shared.Quotations;
using static shared.Equipment.EquipmentDto;

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

  public async Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model)
  {
    var formule = _dbContext.Formulas.FirstOrDefault(formule => formule.Id == model.FormulaId);
    if (formule is null)
    {
      throw new Exception($"Formula with Id: {model.FormulaId}, does not exist");
    }

    var equipmentItems = new List<QuotationLine>();
    foreach (var items in model.Equipments)
    {
      var equipment = _dbContext.Equipments.FirstOrDefault(equipment => equipment.Id == items.EquipmentId);
      equipmentItems.Add(new QuotationLine(new Equipment(equipment.Description.Title, equipment.Price), items.Amount));
    }


    EmailConfiguration emailConfig = EmailConfiguration.GetInstance();
    string mail = emailConfig.Mail;
    string password = emailConfig.Password;

    MailSender mailSender = new MailSender(mail, model.Customer.Email.Email, new System.Net.NetworkCredential(mail, password));

    Quotation quotation = new Quotation(formule, model.StartTime, model.EndTime, model.NumberOfPeople, model.IsTripelBier);
    quotation.Opmerking = model.Opmerking;
    quotation.QuotationLines = equipmentItems;

    mailSender.SendNewQuote(quotation);

    QuotationResponse.Edit result = new QuotationResponse.Edit
    {
      QuotationId = model.QuotationId
    };

    return result;
}
}
