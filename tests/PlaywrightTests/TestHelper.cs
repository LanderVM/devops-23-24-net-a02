using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.PlaywrightTests;

public static class TestHelper
{
  public static string BaseUri = "https://localhost:7276";

  public static string FormulasPage = $"{BaseUri}/Formules";

  public static string ExtraMaterial = $"{BaseUri}/ExtraMateriaal";

  public static string EventDetails = $"{BaseUri}/Event/EventDetails";

  public static string PersonalDetails = $"{BaseUri}/PersoonlijkeDetails";

  public static string Overview = $"{BaseUri}/Overzicht";

  public static string EmailOverview = $"{BaseUri}/EmailOverview";

  public static string QuotationsOverview = $"{BaseUri}/offertes";

  public static string ExtraMaterialAdmin = $"{BaseUri}/ExtraMaterialAdmin";

  public static string FormulasAdmin = $"{BaseUri}/FormulesAdmin";
}
