namespace Shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);
  Task<decimal> GetEstimatedQuotationPrice(QuotationDto.Price model);
}
