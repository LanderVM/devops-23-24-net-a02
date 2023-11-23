using shared.Equipment;

namespace shared.Equipment;

public interface IEquipmentService
{
  public Task<EquipmentResult.Index> GetIndexAsync();

  public Task<int> DeleteAsync(int equipmentId);

  public Task<int> CreateAsync(EquipmentDto.Create model);

  public Task<int> UpdateAsync(int equipmentId,EquipmentDto.Mutate model);
}

