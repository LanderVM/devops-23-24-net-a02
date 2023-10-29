using System.Net;
using System.Net.Mail;
using System.Text;
using Domain.Formulas;

namespace Domain.Common;

public class MailSender
{
  private SmtpClient smtpClient;
  private MailMessage mail;
  private string toAddress;
  private static string apiAdress = "https:/localhost:3000/acceptquote";

  public MailSender(string fromAddress, string toAddress, NetworkCredential networkCredential)
  {
    this.toAddress = toAddress;
    mail = new MailMessage(fromAddress, toAddress);
    smtpClient = new SmtpClient("smtp.office365.com") { Port = 587, Credentials = networkCredential, EnableSsl = true };
  }

  private bool SendMail()
  {
    try
    {
      smtpClient.Send(mail);
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error sending email: " + ex.Message);
      return false;
    }
  }

private string GetMailBody(Formula formula, string comment)
{
    if (formula == null || !formula.Equipment.Any())
    {
        throw new Exception("There is no Equipment in this formula");
    }
    string acceptQuoteUrl = $"{apiAdress}?email={toAddress}";

    StringBuilder contentBuilder = new StringBuilder();
    decimal totalPrice = 0;

    contentBuilder.Append("<div style='font-family: Arial, sans-serif; padding: 20px;'>");
    contentBuilder.Append("<p>Beste,<br>Bedankt voor uw offerteaanvraag bij Blanche.<br>Er zijn enkele wijzigingen aangebracht in uw offerte. Dit is uw bijgewerkte offerte. Gelieve deze te accepteren indien u akkoord bent.</p>");

    contentBuilder.Append("<div style='max-width: 900px; background-color: #fbfbfb; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); padding: 20px; margin-top: 20px;'>");
    contentBuilder.Append("<h2 style='font-size: 18px;'>Offerte</h2>");
    contentBuilder.Append("<table style='width: 100%; border-collapse: collapse; margin-bottom: 10px;'>");
    contentBuilder.Append("<tr style='background-color: #d8d8d8;'><th style='border: 1px solid #ddd; padding: 10px; text-align: left;'>Item</th><th style='border: 1px solid #ddd; padding: 10px; text-align: left;'>Prijs (€)</th></tr>");

    foreach (Equipment equipment in formula.Equipment)
    {
        contentBuilder.Append($"<tr><td style='border: 1px solid #ddd; padding: 10px; text-align: left;'>{equipment.Description.Title}</td><td style='border: 1px solid #ddd; padding: 10px; text-align: left;'>{equipment.Price}</td></tr>");
        totalPrice += equipment.Price;
    }

    contentBuilder.Append($"<tr><td style='border: 1px solid #ddd; padding: 10px; text-align: left;' colspan='2'>Totaal Prijs: {totalPrice} €</td></tr>");
    contentBuilder.Append("</table>");
    contentBuilder.Append($"<p style='margin-bottom: 20px;'>{comment}</p>");
    contentBuilder.Append($"<a href='{acceptQuoteUrl}' style='background-color: #4CAF50; color: white; padding: 15px 32px; text-align: center; font-size: 16px; text-decoration: none; border-radius: 10px; display: inline-block;' target='_blank'>Offerte Accepteren</a></div>");

    contentBuilder.Append("<p>Met vriendelijke groeten,<br>Blanche</p></div>");

    return contentBuilder.ToString();
}

  public void SendNewQuote(Formula formula, string comment)
  {
    mail.Subject = "Offerte Blanche";
    mail.Body = GetMailBody(formula, comment);
    mail.IsBodyHtml = true;

    SendMail();
  }

}
