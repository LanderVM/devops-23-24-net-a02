namespace shared.Quotations;

public interface IQuotationService
{
  Task<QuotationResult.Index> GetIndexAsync();
  Task<QuotationResult.DetailEdit> GetSpecificDetailEditAsync(int quotationId);
  Task<QuotationResult.Dates> GetDatesAsync();

  Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync();
  Task<QuotationResult.Calculation> GetPriceEstimationPrice(QuotationDto.Estimate model);

  Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model);
  Task<QuotationResponse.Create> UpdateAsync(int QuotationId, QuotationDto.Edit model);
}

