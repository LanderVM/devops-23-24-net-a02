using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class HomePageTest:PageTest
{
  [Test]
  public async Task GoToHomeShowThreeFormulaCards()
  {
    await Page.GotoAsync(TestHelper.BaseUri);
    await Page.WaitForSelectorAsync("data-test-id=home-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=home-formula-card");
    var amountOfCards = await Page.Locator("data-test-id=home-formula-card").CountAsync();
    amountOfCards.ShouldBe(3);
  }
}
