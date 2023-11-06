namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{
  public string Naam { get; set; } = "";
  public string Voornaam { get; set; } = "";
  public string Email { get; set; } = "";
  public string TelefoonNummer { get; set; } = "";

  public string Straat { get; set; } = "";
  public string Huisnummer { get; set; } = "";
  public string Gemeente { get; set; } = "";
  public string Postcode { get; set; } = "";

  public string BtwNummer { get; set; } = "";

  public string FacturatieStraat { get; set; } = "";
  public string FacturatieHuisnummer { get; set; } = "";
  public string FacturatieGemeente { get; set; } = "";
  public string FacturatiePostcode { get; set; } = "";

  public bool SwitchValue { get; set; } = true;
}

