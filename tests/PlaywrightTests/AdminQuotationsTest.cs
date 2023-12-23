using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

public class AdminQuotationsTest : PageTest
{
  [Test]
  public async Task GoToQuotationsOverviewCheckQuotationsExists()
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

    await Page.GotoAsync(TestHelper.QuotationsOverview);
    await Page.WaitForSelectorAsync("data-test-id=admin-quotations-grid");
    await Page.WaitForSelectorAsync("data-test-id=admin-quotations-overview-editbutton");
    var amount = await Page.Locator("data-test-id=admin-quotations-overview-editbutton").CountAsync();
    amount.ShouldBeGreaterThan(0);
  }
}
