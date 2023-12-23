namespace devops_23_24_net_a02.Client.Shared;

public static class Routes
{
  public const string About = "/over-ons";
  public const string EventDetails = "/aanvraag/evenement-gegevens";
  public const string PersonalDetails = "/aanvraag/persoonlijke-gegevens";
  public const string Overview = "/aanvraag/overzicht";
  public const string Formula = "/over-ons/formules";
  public const string Home = "/";
  public const string ExtraMaterial = "/aanvraag/extra-materiaal";
  public const string ExtraMaterialReadOnly = "/over-ons/extra-materiaal";

  // Admin
  public const string EmailOverview = "/admin/overzicht/emails";
  public const string FormulasAdmin = "/admin/overzicht/formules";
  public const string ExtraMaterialAdmin = "/admin/overzicht/extra-materiaal";
  public const string Quotations = "/admin/overzicht/offerte-aanvragen";
}
