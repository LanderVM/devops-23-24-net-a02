namespace shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);
  Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync();
  Task<decimal> GetPriceEstimationPrice(QuotationResponse.Estimate model);
  Task<QuotationResult.Index> GetIndexAsync();

  Task<QuotationResult.Dates> GetDatesAsync();
}
