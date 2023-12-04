using shared.Equipment;

namespace shared.Equipment;

public interface IEquipmentService
{
  public Task<EquipmentResult.Index> GetIndexAsync();

  public Task<int> DeleteAsync(int equipmentId);

  public Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model);

  public Task UpdateAsync(int equipmentId, EquipmentDto.Mutate model);

  public Task<EquipmentDto.Index> GetSpecificIndexAsync(int equipmentId);

  public Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId);
}

