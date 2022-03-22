using BookingComUIAndMetaweatherAPITests.BookingComUITests.Pages;
using BookingComUITests.Tests;
using NUnit.Framework;
using System;

namespace BookingComUIAndMetaweatherAPITests.BookingComUITests.Tests
{
    public class BookingUITests : TestBase
    {
        [Test]
        public void ChangeCurrencyFromUSDToEUR()
        {
            HomePage homePage = new HomePage(webDriver);

            webDriver.Url = "https://www.booking.com";
            //Set USD currency as initial state
            if (homePage.GetCurrentCurrencyString() != "USD")
            {
                homePage.SetUSDCurrency();
            }
            homePage.SetEURCurrency();

            Assert.That(homePage.GetCurrentCurrencyString(), Is.EqualTo("EUR"));
        }

        [Test]
        public void ChangeLanguageToDeutsch()
        {
            HomePage homePage = new HomePage(webDriver);

            webDriver.Url = "https://www.booking.com";
            homePage.SetLanguageToDeutsch();
            Assert.That(homePage.GetNameOfStaysButton(), Is.EqualTo("Aufenthalte"));
            Assert.That(homePage.GetCurrentLanguageText, Does.Contain("Ihre aktuelle Sprache ist Deutsch"));
        }

        [Test]
        public void GoFromHomePageToFlightsPage()
        {
            HomePage homePage = new HomePage(webDriver);
            FlightsPage flightsPage = new FlightsPage(webDriver);

            webDriver.Url = "https://www.booking.com/";
            homePage.ClickOnFlightsHeaderLink();

            Assert.That(flightsPage.GetMainTitleText(), Is.EqualTo("Compare and book flights with ease"));
        }

        [Test]
        public void FilterResults()
        {
            HomePage homePage = new HomePage(webDriver);
            SearchResultsPage searchResultsPage = new SearchResultsPage(webDriver);
            var checkInDate = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
            var checkOutDate = DateTime.Today.AddDays(9).ToString("yyyy-MM-dd");

            webDriver.Url = "https://www.booking.com/";
            homePage.SetDestination("Paris");
            homePage.SetDates(checkInDate, checkOutDate);
            homePage.IncreaseNumberOfChildrenAndSetAge(1, 10);
            homePage.ClickOnSearchButton();
            var cities = searchResultsPage.GetCitiesFromResults();
            var guestsAndNightsInfo = searchResultsPage.GetGuestsAndNightsInfoFromResults();

            Assert.That(cities, Is.All.EqualTo("Paris"));
            Assert.That(guestsAndNightsInfo, Is.All.EqualTo("2 nights, 2 adults, 1 child"));
        }
    }
}
