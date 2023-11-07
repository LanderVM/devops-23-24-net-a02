using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Equipment;

public abstract class EquipmentDto
{
  public class Index { 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtext { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<string> ImageData { get; set; }
    public List<int>? FormulaIds { get; set; }
      
  }
}
