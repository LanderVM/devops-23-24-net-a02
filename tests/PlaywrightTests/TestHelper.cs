namespace Client.PlaywrightTests;

public static class TestHelper
{
  public static string BaseUri = "https://localhost:7276";

  public static string FormulasPage = $"{BaseUri}/over-ons/formules";

  public static string ExtraMaterial = $"{BaseUri}/over-ons/extra-materiaal";

  public static string EventDetails = $"{BaseUri}/aanvraag/evenement-gegevens";

  public static string PersonalDetails = $"{BaseUri}/aanvraag/persoonlijke-gegevens";

  public static string Overview = $"{BaseUri}/aanvraag/overzicht";

  public static string EmailOverview = $"{BaseUri}/admin/overzicht/emails";

  public static string QuotationsOverview = $"{BaseUri}/admin/overzicht/offerte-aanvragen";

  public static string ExtraMaterialAdmin = $"{BaseUri}/admin/overzicht/extra-materiaal";

  public static string FormulasAdmin = $"{BaseUri}/admin/overzicht/formules";
}
