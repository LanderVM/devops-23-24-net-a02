using Microsoft.EntityFrameworkCore;
using Shared.Quotations;

namespace Api.Data.Services.Quotations;

public class QuotationService : IQuotationService
{
  private readonly DbContext _dbContext;

  public QuotationService(DbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<int> CreateAsync()
  {
    return 1;
  }
}
