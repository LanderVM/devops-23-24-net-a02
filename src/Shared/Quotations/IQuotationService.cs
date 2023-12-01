namespace Shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);
  Task<QuotationDto.Details> GetPriceEstimationDetails();
  Task<QuotationResult.Index> GetIndexAsync();
}
