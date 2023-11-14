using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Formulas;

public class FormulaResult
{
  public class Index { 
    
    public IEnumerable<FormulaDto.Index>? Formulas { get; set; }

    public int TotalAmount { get; set; }
  }
}

