﻿using System.Net.Mail;
using Domain.Customers;

namespace FormulaTests;

 public class CustomerTest
{
  [Fact]
  public void Create_new_customer_happyFlow()
  {
    Customer customer = new Customer("Jan", "Peeters", new Email("JanPeeters@gmail.com"), new Address("Straat", "01", "Zottegem", "9620"), new PhoneNumber("0479254691"));
    customer.FirstName.ShouldBe("Jan");
    customer.LastName.ShouldBe("Peeters");
    customer.Email.Value.ShouldBeOfType(typeof(MailAddress));
    customer.Email.Value.ToString().ShouldBe("JanPeeters@gmail.com");
    customer.Address.Street.ShouldBe("Straat");
    customer.Address.HouseNumber.ShouldBe("01");
    customer.Address.City.ShouldBe("Zottegem");
    customer.Address.PostalCode.ShouldBe("9620");
    customer.PhoneNumber.Value.ShouldBe("0479254691");
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData(null)]
  [InlineData("JanPeeters")]
  [InlineData("JanPeeters @gmail.com")]
  [InlineData("gmail.com")]
  [InlineData("JanPeeters@gmail.")]
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
  public void Create_new_adress_invalid(string street, string houseNumber, string city, string postalCode)
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
