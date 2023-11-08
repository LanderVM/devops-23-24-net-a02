using System.Net.Mail;
using Domain.Customers;

namespace Domain.Tests;

public class CustomerTest
{
  [Fact]
  public void Create_new_customer_happyFlow()
  {
    var customer = new Customer("Jan", "Peeters", new Email("JanPeeters@gmail.com"),
      new Address("Straat", "01", "Zottegem", "9620"), new PhoneNumber("0479254691"));
    customer.FirstName.ShouldBe("Jan");
    customer.LastName.ShouldBe("Peeters");
    customer.Email.Value.ShouldBeOfType(typeof(MailAddress));
    customer.Email.Value.ToString().ShouldBe("JanPeeters@gmail.com");
    customer.BillingAddress.Street.ShouldBe("Straat");
    customer.BillingAddress.HouseNumber.ShouldBe("01");
    customer.BillingAddress.City.ShouldBe("Zottegem");
    customer.BillingAddress.PostalCode.ShouldBe("9620");
    customer.PhoneNumber.Value.ShouldBe("0479254691");
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData("JanPeeters")]
  [InlineData("gmail.com")]
  public void Create_new_email_invalid(string email)
  {
    Should.Throw<ArgumentException>(() =>
    {
      new Email(email);
    });
  }

  [Theory]
  [InlineData(" ", "01", "Zottegem", "9620")]
  [InlineData("Straat", " ", "Zottegem", "9620")]
  [InlineData(null, "01", "Zottegem", "9620")]
  [InlineData("Straat", null, "Zottegem", "9620")]
  [InlineData("Straat", "01", null, "9620")]
  [InlineData("Straat", "01", "Zottegem", null)]
  public void Create_new_address_invalid(string street, string houseNumber, string city, string postalCode)
  {
    Should.Throw<ArgumentException>(() =>
    {
      new Address(street, houseNumber, city, postalCode);
    });
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData(null)]
  public void Create_new_phoneNumber_invalid(string phoneNumber)
  {
    Should.Throw<ArgumentException>(() =>
    {
      new PhoneNumber(phoneNumber);
    });
  }
}
