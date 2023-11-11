using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Equipment;

public abstract class EquipmentResult
{

  public class Index {

    public IEnumerable<EquipmentDto.Index>? Equipment { get; set; }

    public int TotalAmount { get; set; }
  }
}
