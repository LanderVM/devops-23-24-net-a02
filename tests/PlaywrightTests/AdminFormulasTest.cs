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

[Parallelizable(ParallelScope.Self)]
public class AdminFormulasTest:PageTest
{
  [Test]
  public async Task GoToFormulasAdminOverviewCheckFormulasExists()
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

    await Page.GotoAsync(TestHelper.FormulasAdmin);
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-formulastable");
    await Page.WaitForSelectorAsync("data-test-id=formulas-admin-overview-editbutton");
    int amount = await Page.Locator("data-test-id=formulas-admin-overview-editbutton").CountAsync();
    amount.ShouldBe(3);
  }

  [Test]
  public async Task GoToFormulasAdminOverviewEditFormula()
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
