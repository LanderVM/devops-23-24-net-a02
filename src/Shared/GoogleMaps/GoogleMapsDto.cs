namespace shared.GoogleMaps;

public static class GoogleMapsDto
{
  public class DistanceMatrixResponse
  {
    public List<string>? Destination_Addresses { get; set; }
    public List<string>? Origin_Addresses { get; set; }
    public List<Row>? Rows { get; set; }
    public string? Status { get; set; }
  }

  public class Row
  {
    public List<Element>? Elements { get; set; } = default!;
  }

  public class Element
  {
    public Distance? Distance { get; set; } = default!;
    public Duration? Duration { get; set; } = default!;
    public string? Status { get; set; } = default!;
  }

  public class Distance
  {
    public string? Text { get; set; }
    public int Value { get; set; }
  }

  public class Duration
  {
    public string? Text { get; set; }
    public int Value { get; set; }
  }

  public class Response
  {
    public decimal? DistanceAmount { get; set; }
    public decimal PricePerKm { get; set; }
  }
}
