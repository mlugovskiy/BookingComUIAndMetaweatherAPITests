using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages
{
    public class SearchResultsPage : PageBase
    {
        private IReadOnlyCollection<IWebElement> AddressElementsList => webDriver.FindElements(By.XPath("//span[@data-testid='address']"));
        private IReadOnlyCollection<IWebElement> GuestsAndNightsInfoInResultsElements => webDriver.FindElements(By.XPath("//div[@data-testid='price-for-x-nights']"));

        public SearchResultsPage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public string[] GetCitiesFromResults()
        {
            return AddressElementsList.Select(adressElement => adressElement.Text.Split(",")[^1].Trim()).ToArray();
        }

        public string[] GetGuestsAndNightsInfoFromResults()
        {
            return GuestsAndNightsInfoInResultsElements.Select(guestsInfo => guestsInfo.Text.Trim()).ToArray();
        }
    }
}
