using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class ExtraMaterialPageTest : PageTest
{
  [Test]
  public async Task GoToExtraMaterialShowOneOrMoreCards()
  {
    await Page.GotoAsync(TestHelper.ExtraMaterial);
    await Page.WaitForSelectorAsync("data-test-id=extras-card-overview");
    await Page.WaitForSelectorAsync("data-test-id=extras-extra-card");
    var amountOfCards = await Page.Locator("data-test-id=extras-extra-card").CountAsync();
    amountOfCards.ShouldBeGreaterThan(0);
  }
}
