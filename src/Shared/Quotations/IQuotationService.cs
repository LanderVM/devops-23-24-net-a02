namespace Shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);
  public Task<QuotationDto.Details> GetPriceEstimationDetails();
}
