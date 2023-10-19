namespace Domain.Customer;

public class Customer
{
  public string FirstName { get; }
  public string LastName { get; }
  public Email Email { get; }
  public Adress Address { get; }
  public PhoneNumber PhoneNumber { get; }

  public Customer(string firstName, string lastName, Email email, Adress address, PhoneNumber phoneNumber)
  {
    FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    Email = email;
    Address = address;
    PhoneNumber = phoneNumber;
  }
}
