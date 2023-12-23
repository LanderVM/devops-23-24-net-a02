public class EmailConfiguration
{
  private static EmailConfiguration _instance;

  private EmailConfiguration(string mail, string password)
  {
    Mail = mail;
    Password = password;
  }

  public string Mail { get; }
  public string Password { get; }

  public static EmailConfiguration CreateInstance(string mail, string password)
  {
    if (_instance == null)
    {
      _instance = new EmailConfiguration(mail, password);
    }

    return _instance;
  }

  public static EmailConfiguration GetInstance()
  {
    return _instance;
  }
}
