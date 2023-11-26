namespace shared.Equipment;

public static class EquipmentResult
{

  public class Index {

    public IEnumerable<EquipmentDto.Index>? Equipment { get; set; }

    public int TotalAmount { get; set; }
  }
}
