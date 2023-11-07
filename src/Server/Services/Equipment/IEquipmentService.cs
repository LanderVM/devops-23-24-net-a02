using shared.Equipment;

namespace server.Services;

public interface IEquipmentService
{
  public Task<EquipmentResult.Index> GetIndexAsync();
}

