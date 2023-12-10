namespace shared.Quotations;

public interface IQuotationService
{
  Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model);
  Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync();
  Task<decimal> GetPriceEstimationPrice(QuotationResponse.Estimate model);
  Task<QuotationResult.Index> GetIndexAsync();

  Task<QuotationResult.Dates> GetDatesAsync();
}
