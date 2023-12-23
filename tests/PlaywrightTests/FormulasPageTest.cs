using Microsoft.Playwright.NUnit;
using Shouldly;

namespace Client.PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class FormulasPageTest : PageTest
{
  [Test]
  public async Task GoToFormulasShowThreeFormulaCards()
  {
    await Page.GotoAsync(TestHelper.FormulasPage);
    await Page.WaitForSelectorAsync("data-test-id=formulas-cards-overview");
    await Page.WaitForSelectorAsync("data-test-id=formulas-formula-card");
    var amountOfCards = await Page.Locator("data-test-id=formulas-formula-card").CountAsync();
    amountOfCards.ShouldBe(3);
  }
}
