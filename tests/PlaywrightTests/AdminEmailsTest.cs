using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class AdminEmailsTest : PageTest
{
  [Test]
  public async Task GoToEmailsOverviewCheckEmailsExists()
  {
    //DotNetEnv.Env.Load();

    //DotNetEnv.Env.Load();

    //string email = System.Environment.GetEnvironmentVariable("EMAIL");
    //string password = System.Environment.GetEnvironmentVariable("PASSWORD");

    //string email = DotNetEnv.Env.GetString("EMAIL");
    //string password = DotNetEnv.Env.GetString("PASSWORD");

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
  }

}
