namespace shared.Quotations;

public interface IQuotationService
{
  Task<QuotationResult.Index> GetIndexAsync();
  Task<QuotationResult.Dates> GetDatesAsync();

  Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync();
  Task<decimal> GetPriceEstimationPrice(QuotationDto.Estimate model);

  Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model);
  Task<QuotationResponse.Create> UpdateAsync(int QuotationId, QuotationDto.Edit model);
}

