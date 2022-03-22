using OpenQA.Selenium;

namespace BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages
{
    public abstract class PageBase
    {
        internal IWebDriver webDriver;
        public PageBase(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
    }
}
