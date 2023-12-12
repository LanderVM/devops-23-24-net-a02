using System.Net;
using System.Net.Mail;
using System.Text;
using Domain.Formulas;
using Domain.Quotations;

namespace Domain.Common;

public class MailSender
{
  private SmtpClient smtpClient;
  private MailMessage mail;
  private string toAddress;
  private static string apiAdress = "https:/localhost:3000/acceptquote";
  private string acceptQuoteUrl;

  public MailSender(string fromAddress, string toAddress, NetworkCredential networkCredential)
  {
    this.toAddress = toAddress;
    mail = new MailMessage(fromAddress, toAddress);
    smtpClient = new SmtpClient("smtp.office365.com") { Port = 587, Credentials = networkCredential, EnableSsl = true };
    acceptQuoteUrl = $"{apiAdress}?email={toAddress}";
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

  private string GetMailBody(Quotation quotation)
  {
    /*var formula = quotation.Formula;
    if (formula == null || !formula.Equipment.Any())
    {
      throw new Exception("There is no Equipment in this formula");
    }*/
    StringBuilder contentBuilder = new StringBuilder();

    decimal totalPrice = 0;

    // Bedrijfsinformatie
    string bedrijfsnaam = "BLANCHE Mobiele Bar";
    string contactpersoon = "Willem Dewaele";
    string adres = "Albert Liénartstraat 199, 300 Aalst";
    string btwNummer = "BTW: 0825.292.925";

    // Klantinformatie
    string bedrijfsnaamProvincie = "Provincie West-Vlaanderen";
    string contactpersoonProvincie = "Boeverbos";
    string adresProvincie = "Koning Leopold III-laan";
    string adresStad = "8200 Brugge Sint-Andries";
    string btwNummerProvincie = "BTW: BE 02 07 725 696";

    // Oferte
    string factuurNummer = "2023017";
    string datum = "24/06/2023";
    string afbeeldingUrl = "";

    decimal btw12 = 100m * 0.12m; //replace 100m with price with 12% btw
    decimal btw21 = 35.99m * 0.21m; //replace 35.99m with price with 12% btw
    decimal totaal = totalPrice + btw12 + btw21;

    contentBuilder.Append("<div style='font-family: Arial, sans-serif; padding: 20px;'>");
    contentBuilder.Append("<p>Beste,<br>Bedankt voor uw offerteaanvraag bij Blanche.<br>Er zijn enkele wijzigingen aangebracht in uw offerte. Dit is uw bijgewerkte offerte. Gelieve deze te accepteren indien u akkoord bent.</p>");
    contentBuilder.Append("<div style='background-color: #fbfbfb; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); padding: 20px; margin-top: 20px;'>");

    // Bedrijfsinformatie en afbeelding
    contentBuilder.Append($"<img src='{afbeeldingUrl}' alt='Bedrijfslogo' style='float: left; height: 220'>");
    contentBuilder.Append("<div style='float: right; '>");
    contentBuilder.Append($"<p style='margin: 0'>{bedrijfsnaam}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{contactpersoon}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{adres}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{btwNummer}</p>");
    // Bedrijfsinformatie Provincie met zwarte rand
    contentBuilder.Append("<div style='border: 2px solid black; padding: 10px; margin-top: 20px; float: left; text-align: right;'>");
    contentBuilder.Append($"<p style='margin: 0'>AAN:</p>");
    contentBuilder.Append($"<p style='margin: 0'>{bedrijfsnaamProvincie}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{adresStad}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{contactpersoonProvincie}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{adresProvincie}</p>");
    contentBuilder.Append($"<p style='margin: 0'>{btwNummerProvincie}</p>");
    contentBuilder.Append("</div>");
    contentBuilder.Append("</div>");
    contentBuilder.Append("<div style='clear: both;'></div>");

    contentBuilder.Append("<h3>OFFERTE</h3>");
    contentBuilder.Append($"<p style='margin: 0 0 6'>Factuurnr. {factuurNummer}</p>");
    contentBuilder.Append($"<p style='margin: 0 0 6'>Datum {datum} </p>");

    contentBuilder.Append("<table style='width: 100%; border-collapse: collapse; margin-bottom: 10px;'>");
    contentBuilder.Append("<tr style='background-color: #d8d8d8;'><th style='border: 1px solid #ddd; padding: 10px;'>Aantal</th><th style='border: 1px solid #ddd; padding: 10px;'>Omschrijving</th><th style='border: 1px solid #ddd; padding: 10px;'>Eenhprijs (€)</th><th style='border: 1px solid #ddd; padding: 10px;'>Bedrag (€)</th><th style='border: 1px solid #ddd; padding: 10px;'>BTW</th></tr>");

    foreach (Equipment equipment in quotation.QuotationLines.Select(x => x.EquipmentOrdered).ToList())
    {
      int aantal = 2;
      decimal btwPercentageItem = 0.21m;

      decimal eenheidsprijs = equipment.Price;
      decimal bedrag = eenheidsprijs * aantal;
      decimal btw = bedrag * btwPercentageItem;

      contentBuilder.Append("<tr>");
      contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px; text-align: center; width: 12%'>{aantal}</td>");
      contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px; width: 39%;'>{equipment.Description.Title}</td>");
      contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px; text-align: center; width: 14%;'>{eenheidsprijs.ToString("C2")}</td>");
      contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px; text-align: center; width: 13%;'>{bedrag.ToString("C2")}</td>");
      contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px; text-align: center; width: 22%;'>{btw.ToString("C2")}</td>");
      contentBuilder.Append("</tr>");

      totalPrice += bedrag;
    }

    contentBuilder.Append("<tr style='background-color: #d8d8d8; font-weight: bold; text-align: center;'>");
    contentBuilder.Append("<td style='border: 1px solid #ddd; padding: 10px;'>Belastbaar</td>");
    contentBuilder.Append("<td style='border: 1px solid #ddd; padding: 10px;'>BTW 12%</td>");
    contentBuilder.Append("<td style='border: 1px solid #ddd; padding: 10px;'>BTW 21%</td>");
    contentBuilder.Append("<td style='border: 1px solid #ddd; padding: 10px;' colspan='2'>TOTAAL</td>");
    contentBuilder.Append("</tr>");

    contentBuilder.Append("<tr style='text-align: center;'>");
    contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px;'>{totalPrice.ToString("C2")}</td>");
    contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px;'>{btw12.ToString("C2")}</td>");
    contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px;'>{btw21.ToString("C2")}</td>");
    contentBuilder.Append($"<td style='border: 1px solid #ddd; padding: 10px;' colspan='2'>{totaal.ToString("C2")}</td>");
    contentBuilder.Append("</tr>");

    contentBuilder.Append("</table>");

    contentBuilder.Append($"<p style='margin-bottom: 20px;'>Comment: <br>{quotation.Opmerking}</p>");
    contentBuilder.Append($"<a href='{acceptQuoteUrl}' style='background-color: #4CAF50; color: white; padding: 15px 32px; text-align: center; font-size: 16px; text-decoration: none; border-radius: 10px; display: inline-block;' target='_blank'>Offerte Accepteren</a></div>");

    contentBuilder.Append("<p>Met vriendelijke groeten,<br>Blanche</p></div>");

    return contentBuilder.ToString();
  }

  public bool SendNewQuote(Quotation quotation)
  {    
    mail.Subject = "Offerte Blanche";
    mail.Body = GetMailBody(quotation);
    //mail.Body = "Dit is een test";
    mail.IsBodyHtml = true;

    return SendMail();
  }

}
