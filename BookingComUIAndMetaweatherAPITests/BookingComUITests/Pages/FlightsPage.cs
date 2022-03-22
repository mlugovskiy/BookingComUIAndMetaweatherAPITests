using OpenQA.Selenium;

namespace BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages
{
    public class FlightsPage : PageBase
    {
        private IWebElement MainTitle => webDriver.FindElement(By.XPath("(//div[contains(@class, 'Text-module__root--variant-headline')])[1]"));
        private IWebElement FlightSearchForm => webDriver.FindElement(By.XPath("//div[contains(@class, 'Flights-Search-StyleJamFlightSearchForm')]"));
        
        public FlightsPage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public bool FlightFormPresence()
        {
            return FlightSearchForm.Displayed;
        }

        public string GetMainTitleText()
        {
            return MainTitle.Text;
        }
    }
}
