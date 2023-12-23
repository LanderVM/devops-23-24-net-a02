using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class AdminEmailsTest : PageTest
{
  [Test]
  public async Task GoToEmailsOverviewCheckEmailsExists()
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

    await Page.GotoAsync(TestHelper.EmailOverview);
    await Page.WaitForSelectorAsync("data-test-id=email-table");
    await Page.WaitForSelectorAsync("data-test-id=email-row");
    var amount = await Page.Locator("data-test-id=email-row").CountAsync();
    amount.ShouldBeGreaterThan(0);
  }
}
