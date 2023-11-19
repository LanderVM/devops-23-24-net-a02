namespace Shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);
  Task<decimal> GetPriceAsync(QuotationDto.Price model);
}
