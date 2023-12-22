using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Internal;
using Shouldly;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.PlaywrightTests;

public class AdminQuotationsTest:PageTest
{
  [Test]
  public async Task GoToQuotationsOverviewCheckQuotationsExists()
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

    await Page.GotoAsync(TestHelper.QuotationsOverview);
    await Page.WaitForSelectorAsync("data-test-id=admin-quotations-grid");
    await Page.WaitForSelectorAsync("data-test-id=admin-quotations-overview-editbutton");
    int amount =await Page.Locator("data-test-id=admin-quotations-overview-editbutton").CountAsync();
    amount.ShouldBeGreaterThan(0);
  }
}
