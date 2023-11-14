using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Formulas;

public interface IFormulaService
{
  public Task<FormulaResult.Index> GetIndexAsync();
}

