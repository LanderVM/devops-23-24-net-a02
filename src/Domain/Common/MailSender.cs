using System.Net.Mail;
using System.Net;

namespace Domain.Common;
public class MailSender
{
  public MailMessage Mail { get; }
  private SmtpClient smtpClient;

  public MailSender(string fromAddress, string toAddress, NetworkCredential networkCredential)
  {
    Mail = new MailMessage(fromAddress, toAddress);
    smtpClient = new SmtpClient("smtp.office365.com")
    {
      Port = 587,
      Credentials = networkCredential,
      EnableSsl = true
    };
  }

  public void SetMailContent(string subject, string body, bool isHtml = true)
  {
    Mail.Subject = subject;
    Mail.Body = body;
    Mail.IsBodyHtml = isHtml;
  }

  public void AddAttachment(Attachment attachment)
  {
    Mail.Attachments.Add(attachment);
  }

  public bool SendMail()
  {
    try
    {
      smtpClient.Send(Mail);
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error sending email: " + ex.Message);
      return false;
    }
  }
  private void Example()
  {
    string content = "<p>Beste,<br>Bedankt voor uw aanvraag van een offerte bij Blanche.<br>Wij laten u verder nog iets weten indien deze is goedgekeurd.<br>Met vriendelijke groeten,<br>Blanche</p>";

    string from = "";
    string to = "";
    string email = "";
    string password = "";


    MailSender mailSender = new MailSender(from, to, new NetworkCredential(email, password));
    mailSender.SetMailContent("Onderwerp", content);
    mailSender.SendMail();
  }
}
