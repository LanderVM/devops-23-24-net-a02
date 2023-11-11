namespace Domain.Common;

public abstract class Entity
{
  public int Id { get; protected set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}
