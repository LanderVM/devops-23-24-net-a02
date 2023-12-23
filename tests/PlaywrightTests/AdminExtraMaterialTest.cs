using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

public class AdminExtraMaterialTest : PageTest
{
  [Test]
  public async Task GoToExtraMaterialAdminCheckExtraMaterialExists()
  {
    var email = ClientTestCredentials.email;
    var password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Continue" }).ClickAsync();

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
    var email = ClientTestCredentials.email;
    var password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");

    var initialAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    await Page.Locator("data-test-id=extras-admin-extra-card-remove-button").Last.ClickAsync();
    await Page.Locator("data-test-id=extras-admin-extra-card-remove-button-definitly").Last.ClickAsync();

    await Page.WaitForTimeoutAsync(7000);

    var eventualAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    eventualAmount.ShouldBe(initialAmount - 1);
  }

  [Test]
  public async Task GoToExtraMaterialAdminAddCard()
  {
    var email = ClientTestCredentials.email;
    var password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");

    var initialAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    await Page.Locator("data-test-id=extras-admin-create-button").ClickAsync();
    await Page.Locator("data-test-id=admin-extra-create-title").FillAsync("Bucket");
    await Page.Locator("data-test-id=admin-extra-create-price").FillAsync("43.72");
    await Page.Locator("data-test-id=admin-extra-create-stock").FillAsync("4");
    await Page.Locator("data-test-id=admin-extra-create-attributes").FillAsync("spoon;soap");
    await Page.Locator("data-test-id=admin-extra-create-createbutton").ClickAsync();


    await Page.WaitForTimeoutAsync(7000);

    var eventualAmount = await Page.Locator("data-test-id=extras-admin-extra-card").CountAsync();

    eventualAmount.ShouldBe(initialAmount + 1);

    await Page.GetByText("Bucket").IsVisibleAsync();
    await Page.GetByText("43.72").IsVisibleAsync();
  }

  [Test]
  public async Task GoToExtraMaterialAdminUpdateCard()
  {
    var email = ClientTestCredentials.email;
    var password = ClientTestCredentials.password;

    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=admin-button");
    await Page.Locator("data-test-id=admin-button").ClickAsync();

    await Page.WaitForTimeoutAsync(7000);
    await Page.GetByLabel("Email address").FillAsync(email);
    await Page.GetByLabel("Password").FillAsync(password);
    await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Continue" }).ClickAsync();

    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");

    await Page.GotoAsync(TestHelper.ExtraMaterialAdmin);
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-admin-extra-card");

    await Page.Locator("data-test-id=extras-admin-extra-card-update-button").Last.ClickAsync();

    await Page.Locator("data-test-id=admin-extra-edit-title").FillAsync("Gun");
    await Page.Locator("data-test-id=admin-extra-edit-price").FillAsync("23.66");
    await Page.Locator("data-test-id=admin-extra-edit-stock").FillAsync("6");
    await Page.Locator("data-test-id=admin-extra-edit-attributes").FillAsync("powder;bullets");

    await Page.Locator("data-test-id=extras-admin-edit-editbutton").Last.ClickAsync();

    await Page.GetByText("Gun").IsVisibleAsync();
    await Page.GetByText("23.66").IsVisibleAsync();
  }
}
