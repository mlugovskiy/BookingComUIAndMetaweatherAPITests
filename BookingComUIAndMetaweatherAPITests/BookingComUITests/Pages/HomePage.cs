using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages
{
    public class HomePage : PageBase
    {
        //Accept Cookies Button
        private IReadOnlyList<IWebElement> AcceptCookiesButtonList => webDriver.FindElements(By.XPath("//button[@id='onetrust-accept-btn-handler']"));

        //Name Element of "Stays" button
        private IWebElement StaysButtonName => webDriver.FindElement(By.XPath("(//header//span[@class='bui-tab__text'])[1]"));

        //Flights header link
        private IWebElement FlightsHeaderLink => webDriver.FindElement(By.XPath("//a[contains(@data-decider-header, 'flights')]"));
        
        //Main Title
        private IWebElement MainTitle => webDriver.FindElement(By.XPath("//div[@data-testid='herobanner-title1']"));
        
        //Currency Elements
        private IWebElement CurrencyListButton => webDriver.FindElement(By.XPath("//button[contains(@data-modal-header-async-type, 'currencyDesktop')]"));
        private IWebElement USDCurrencyButton => webDriver.FindElement(By.XPath("(//div[@class='bui-traveller-header__selection-list'])[2]//a[contains(@data-modal-header-async-url-param, 'selected_currency=USD')]"));
        private IWebElement CurrentCurrency => webDriver.FindElement(By.XPath("//button[contains(@data-modal-header-async-type, 'currencyDesktop')]/span/span[1]"));
        private IWebElement EURCurrencyButton => webDriver.FindElement(By.XPath("(//div[@class='bui-traveller-header__selection-list'])[2]//a[contains(@data-modal-header-async-url-param, 'selected_currency=EUR')]"));
        
        //Language Elements
        private IWebElement ChangeLanguageButton => webDriver.FindElement(By.XPath("//button[contains(@data-modal-id, 'language-selection')]"));
        private IWebElement EnglishUKLanguageButton => webDriver.FindElement(By.XPath("(//div[@class='bui-traveller-header__selection-list'])[2]//a[contains(@data-lang, 'en-gb')]"));
        private IWebElement DeutschLanguageButton => webDriver.FindElement(By.XPath("(//div[@class='bui-traveller-header__selection-list'])[2]//a[contains(@data-lang, 'de')]"));
        private IWebElement CurrentLanguageSpan => webDriver.FindElement(By.XPath("//button[@data-modal-id='language-selection']/span/span"));

        //Search Form
        private IWebElement DestinationInput => webDriver.FindElement(By.XPath("//input[@type='search']"));
        private IWebElement DatesField => webDriver.FindElement(By.XPath("//div[contains(@class, 'xp__fieldset')]/div[contains(@class, 'xp__group')]"));
        private IWebElement GuestsCountField => webDriver.FindElement(By.XPath("//div[@data-component='search/group/group-with-modal']"));
        private IWebElement IncreaseNumberOfChildrenButton => webDriver.FindElement(By.XPath("//button[@aria-label='Increase number of Children']"));
        private IWebElement SelectChildrenAgeElement => webDriver.FindElement(By.XPath("//select[@name='age']"));
        private IWebElement SearchButton => webDriver.FindElement(By.XPath("//button[@class='sb-searchbox__button ']"));

        public HomePage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public void SetUSDCurrency()
        {
            CurrencyListButton.Click();
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", USDCurrencyButton);
            USDCurrencyButton.Click();
        }

        public void SetEURCurrency()
        {
            CurrencyListButton.Click();
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", EURCurrencyButton);
            EURCurrencyButton.Click();
        }

        public string GetCurrentCurrencyString()
        {
            return CurrentCurrency.Text;
        }

        public void ClickOnFlightsHeaderLink()
        {
            FlightsHeaderLink.Click();
        }

        public void SetLanguageToEnglishUK()
        {
            ChangeLanguageButton.Click();
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", EnglishUKLanguageButton);
            EnglishUKLanguageButton.Click();
        }

        public void SetLanguageToDeutsch()
        {
            ChangeLanguageButton.Click();
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", DeutschLanguageButton);
            DeutschLanguageButton.Click();
        }

        public string GetPageMainTitleString()
        {
            return MainTitle.Text;
        }

        public void SetDestination(string destination)
        {
            DestinationInput.SendKeys(destination);
        }

        public void SetDates(string checkInDate, string checkOutDate)
        {
            DatesField.Click();
            var checkInDateEl = webDriver.FindElement(By.XPath($"//td[@data-date=\"{checkInDate}\"]"));
            var checkOutDateEl = webDriver.FindElement(By.XPath($"//td[@data-date=\"{checkOutDate}\"]"));

            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", checkInDateEl);
            checkInDateEl.Click();

            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", checkOutDateEl);
            checkOutDateEl.Click();
        }

        public void IncreaseNumberOfChildrenAndSetAge(int increaseNumber, int age)
        {
            GuestsCountField.Click();
            for (int i = 1; i <= increaseNumber; i++)
            {
                IncreaseNumberOfChildrenButton.Click();
            }
            var SelectChildrenAgeObject = new SelectElement(SelectChildrenAgeElement);
            SelectChildrenAgeObject.SelectByValue(age.ToString());
        }

        public void ClickOnSearchButton()
        {
            SearchButton.Click();
        }

        public string GetCurrentLanguageText()
        {
            return CurrentLanguageSpan.Text;
        }

        public bool AcceptCookiesButtonPresence()
        {
            if (AcceptCookiesButtonList.Count == 0)
            {
                return false;
            }
            return AcceptCookiesButtonList[0].Displayed;
        }

        public void ClickOnAcceptCookiesButton()
        {
            AcceptCookiesButtonList[0].Click();
        }

        public string GetNameOfStaysButton()
        {
            return StaysButtonName.Text;
        }
    }
}
