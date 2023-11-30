namespace Shared.Quotations;

public static class QuotationResult
{

  public class Index {

    public IEnumerable<QuotationDto.Index>? Quotation { get; set; }

    public int TotalAmount { get; set; }
  }
}
