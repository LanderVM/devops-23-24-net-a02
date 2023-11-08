using shared.Equipment;

namespace shared.Equipment;

public interface IEquipmentService
{
  public Task<EquipmentResult.Index> GetIndexAsync();
}

