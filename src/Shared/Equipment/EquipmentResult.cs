namespace shared.Equipment;

public static class EquipmentResult
{
  public class Index
  {
    public IEnumerable<EquipmentDto.Index>? Equipment { get; set; }

    public int TotalAmount { get; set; }
  }

  public class ActiveEquipment
  {
    public IEnumerable<EquipmentDto.Index>? Equipment { get; set; }

    public int TotalAmount { get; set; }
  }

  public class Create
  {
    public int Id { get; set; }
  }

  public class CreateWithImage
  {
    public ImageData? Image { get; set; }
    public int Id { get; set; }
  }
}
