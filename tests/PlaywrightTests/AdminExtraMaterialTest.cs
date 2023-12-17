using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

public class AdminExtraMaterialTest:PageTest
{
  [Test]
  public async Task GoToExtraMaterialAdminCheckExtraMaterialExists()
  {
    string email = ClientTestCredentials.email;
    string password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");
    var amountOfCards = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();
    amountOfCards.ShouldBeGreaterThan(0);
  }

  [Test]
  public async Task GoToExtraMaterialAdminRemoveCard()
  {
    string email = ClientTestCredentials.email;
    string password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");
    
    int initialAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    await Page.Locator("data-test-id=extras-admin-extra-card-remove-button").Last.ClickAsync();
    await Page.Locator("data-test-id=extras-admin-extra-card-remove-button-definitly").Last.ClickAsync();

    await Page.WaitForTimeoutAsync(7000);

    int eventualAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    eventualAmount.ShouldBe(initialAmount-1);
  }

  [Test]
  public async Task GoToExtraMaterialAdminAddCard()
  {
    string email = ClientTestCredentials.email;
    string password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");
    //var amountOfCards = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();
    //amountOfCards.ShouldBeGreaterThan(0);
    int initialAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    await Page.Locator("data-test-id=extras-admin-create-button").ClickAsync();
    await Page.Locator("data-test-id=admin-extra-create-title").FillAsync("Bucket");
    await Page.Locator("data-test-id=admin-extra-create-price").FillAsync("5.55");
    await Page.Locator("data-test-id=admin-extra-create-stock").FillAsync("4");
    await Page.Locator("data-test-id=admin-extra-create-attributes").FillAsync("spoon;soap");
    await Page.Locator("data-test-id=admin-extra-create-createbutton").ClickAsync();
    

    await Page.WaitForTimeoutAsync(7000);

    int eventualAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    eventualAmount.ShouldBe(initialAmount + 1);
  }
}
