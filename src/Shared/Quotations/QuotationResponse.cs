namespace Shared.Quotations;

public static class QuotationResponse
{
  public class Create
  {
    public int QuotationId { get; set; }
  }
  public class Price
  {
    public decimal EstimatedPrice { get; set; }
  }
}
