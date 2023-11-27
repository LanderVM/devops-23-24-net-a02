using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Equipment;

public static class EquipmentDto
{
  public class Index { 
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> Attributes { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ImageData ImageData { get; set; }
    public List<int>? FormulaIds { get; set; }
      
  }
  public class ImageData {
      public string ImageUrl { get; set; }

      public string AltText { get; set; }
  }

  public class Select
  {
    public int Id { get; set; }
    public string Title { get; set; } = default!;
  }
  public class Ids
  {
    public List<int> Id { get; set; } = default!;
  }
}
