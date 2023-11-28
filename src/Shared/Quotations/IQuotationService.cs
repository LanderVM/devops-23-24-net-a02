namespace shared.Quotations;

public interface IQuotationService
{
  Task<int> CreateAsync(QuotationDto.Create model);

  Task<List<DateTime>> GetDatesAsync();
}
