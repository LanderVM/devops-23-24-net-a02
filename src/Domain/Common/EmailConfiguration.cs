public class EmailConfiguration
{
  private static EmailConfiguration _instance;
  public string Mail { get; }
  public string Password { get; }

  private EmailConfiguration(string mail, string password)
  {
    Mail = mail;
    Password = password;
  }

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
