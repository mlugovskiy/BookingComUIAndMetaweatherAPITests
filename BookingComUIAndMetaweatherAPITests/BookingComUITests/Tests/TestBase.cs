using BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BookingComUITests.Tests
{
    public class TestBase
    { 
        public IWebDriver webDriver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            webDriver.Url = "https://www.booking.com";
            HomePage homePage = new HomePage(webDriver);

            //Close Accept Cookies modal window
            if (homePage.AcceptCookiesButtonPresence())
            {
                homePage.ClickOnAcceptCookiesButton();
            }
        }

        [SetUp]
        public void SetUp()
        {
            HomePage homePage = new HomePage(webDriver);

            //Check English UK language is set for site
            if (!homePage.GetCurrentLanguageText().Contains("Your current language is English (UK)"))
            {
                homePage.SetLanguageToEnglishUK();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }
    }
}
