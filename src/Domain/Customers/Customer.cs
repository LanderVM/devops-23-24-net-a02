using Domain.Formulas;

namespace Domain.Customer;

public class Customer
{
  public Formula Formula { get; set; } 
  public string FirstName { get; }
  public string LastName { get; }
  public Email Email { get; }
  public Adress Address { get; }
  public PhoneNumber PhoneNumber { get; }

  public Customer(string firstName, string lastName, Email email, Adress address, PhoneNumber phoneNumber) : this(firstName, lastName, email, address, phoneNumber, default!) 
  { 
  }
  public Customer(string firstName, string lastName, Email email, Adress address, PhoneNumber phoneNumber, Formula formula)
  {
    FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    Email = email;
    Address = address;
    PhoneNumber = phoneNumber;
    Formula = formula;
  }
}
