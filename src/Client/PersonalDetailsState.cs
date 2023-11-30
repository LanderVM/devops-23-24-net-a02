namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string Email { get; set; } = "";
  public string PhoneNumber { get; set; } = "";

  public string Street { get; set; } = "";
  public string Housenumber { get; set; } = "";
  public string City { get; set; } = "";
  public string PostalCode { get; set; } = "";

  public string BtwNumber { get; set; } = "";

  public string BillingStreet { get; set; } = "";
  public string BillingHouseNumber { get; set; } = "";
  public string BillingCity { get; set; } = "";
  public string BillingPostalCode { get; set; } = "";

  public bool SwitchValue { get; set; } = true;
}

