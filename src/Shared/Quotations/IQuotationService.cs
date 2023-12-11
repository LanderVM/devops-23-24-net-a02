namespace shared.Quotations;

public interface IQuotationService
{
  Task<QuotationResult.Index> GetIndexAsync();
  Task<QuotationResult.Dates> GetDatesAsync();
  Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model);
  Task<QuotationResponse.Edit> UpdateAsync(int QuotationId, QuotationDto.Edit model);

  Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync();
  Task<decimal> GetPriceEstimationPrice(QuotationResponse.Estimate model);
}
