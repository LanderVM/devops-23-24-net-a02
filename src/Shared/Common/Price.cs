using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common;

public class PriceDto
{
  public int Id { get; set; }
  public List<decimal> Price { get; set; } = default!;
}

