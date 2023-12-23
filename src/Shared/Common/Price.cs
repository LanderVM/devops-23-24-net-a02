namespace Shared.Common;

public class PriceDto
{
  public int Id { get; set; }
  public List<decimal> Price { get; set; } = default!;
}
