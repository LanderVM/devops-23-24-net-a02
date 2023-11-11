using Shared.Common;

namespace Shared.Quotations;

public abstract class QuotationDto
{
  public class CreateQuotation
  {
    public AddressDto EventLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public CustomerDto.CreateCustomer Customer { get; set; }
  }
}
