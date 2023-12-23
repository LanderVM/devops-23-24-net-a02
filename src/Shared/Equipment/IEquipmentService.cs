namespace shared.Equipment;

public interface IEquipmentService
{
  public Task<EquipmentResult.Index> GetIndexAsync();

  public Task<EquipmentResult.ActiveEquipment> GetActiveEquipmentAsync();

  public Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId);

  public Task<int> DeleteAsync(int equipmentId);

  public Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model);
  public Task<EquipmentResult.CreateWithImage> CreateWithImageAsync(EquipmentDto.Create model);

  public Task<EquipmentResult.Create> UpdateAsync(int equipmentId, EquipmentDto.Mutate model);
  public Task<EquipmentResult.CreateWithImage> UpdateWithImageAsync(int equipmentId, EquipmentDto.Mutate model);
}
