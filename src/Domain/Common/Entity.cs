namespace Domain.Common;

// TODO referenties van objecten die een relatie hebben in db bijhouden in klasses (zie voorbeeld Image <-> Equiment)
public abstract class Entity
{
  public int Id { get; protected set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  
}
