using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class AdminFormulasTest : PageTest
{
  [Test]
  public async Task GoToFormulasAdminOverviewCheckFormulasExists()
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

    await Page.GotoAsync(TestHelper.FormulasAdmin);
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-formulastable");
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-overview-editbutton");
    var amount = await Page.Locator("data-test-id=formulas-admin-overview-editbutton").CountAsync();
    amount.ShouldBe(3);
  }

  [Test]
  public async Task GoToFormulasAdminOverviewEditFormula()
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

    await Page.GotoAsync(TestHelper.FormulasAdmin);
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-formulastable");
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-overview-editbutton");
    await Page.Locator("data-test-id=formulas-admin-overview-editbutton").Last.ClickAsync();

    await Page.Locator("data-test-id=formulas-admin-edit-title").FillAsync("Special offer");
    await Page.Locator("data-test-id=formulas-admin-edit-pricePerDayExtra").FillAsync("24");

    await Page.Locator("data-test-id=formulas-admin-edit-editbutton").ClickAsync();

    await Page.GetByText("Special offer").IsVisibleAsync();
    await Page.GetByText("24").IsVisibleAsync();
  }
}
