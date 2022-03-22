using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookingComUIAndMetaweatherAPITests.MetawheatherAPITests.Tests
{
    internal class HttpMetawheatherComTests
    {
        int cityMinskWoeid;

        [OneTimeSetUp]
        public void ReceiveCityMinskWoeid()
        {
            var url = "https://www.metaweather.com/api/location/search/?query=min";
            var jsonString = GetJsonStringFromApi(url);
            var cityMinskData = ExtractCityDataFromSearchResponse(jsonString, "Minsk");
            cityMinskWoeid = cityMinskData.Woeid;
        }

        [Test]
        public void QueryMethodReturnsLatitudeLongitude()
        {
            const double latitudeOfMinskExpected = 53.90255;
            const double longitudeOfMinskExpected = 27.563101;
            var url = "https://www.metaweather.com/api/location/search/?query=min";

            var jsonString = GetJsonStringFromApi(url);
            var cityMinskData = ExtractCityDataFromSearchResponse(jsonString, "Minsk");
            //latitude
            var latitudeString = cityMinskData.LatLong.Split(',')[0];
            double latitudeOfMinskReceived = double.Parse(latitudeString, CultureInfo.InvariantCulture);
            //longitude
            var longitudeString = cityMinskData.LatLong.Split(",")[1];
            double longitudeOfMinskReceived = double.Parse(longitudeString, CultureInfo.InvariantCulture);

            Assert.That(latitudeOfMinskReceived, Is.EqualTo(latitudeOfMinskExpected));
            Assert.That(longitudeOfMinskReceived, Is.EqualTo(longitudeOfMinskExpected));
        }

        [Test]
        public void MinskWheatherTemperatureInRangeTest()
        {
            const double minTempExpected = -5;
            const double maxTempExpected = 15;
            var baseEndpoint = "https://www.metaweather.com/api/location/";
            var endpointWithMinskWoeid = baseEndpoint + cityMinskWoeid;

            var jsonString = GetJsonStringFromApi(endpointWithMinskWoeid);
            var wheatherTodayAnd5DaysList = JsonSerializer.Deserialize<WeatherRootObject>(jsonString).WheatherTodayAnd5DaysList;

            foreach (var wheather in wheatherTodayAnd5DaysList)
            {
                Assert.That(wheather.The_temp, Is.InRange(minTempExpected, maxTempExpected));
                Assert.That(wheather.Min_temp, Is.InRange(minTempExpected, maxTempExpected));
                Assert.That(wheather.Max_temp, Is.InRange(minTempExpected, maxTempExpected));
            }
        }

        [Test]
        public void AtLeastOneWeatherState5YearsAgoMatchesWithToday()
        {
            var baseEndpoint = "https://www.metaweather.com/api/location/";
            var endpointWithMinskWoeid = baseEndpoint + cityMinskWoeid;
            string date5YearsAgo = DateTime.Today.AddYears(-5).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string endpointWithMinskWoeidAnd5YearsAgoDate = endpointWithMinskWoeid + "/" + date5YearsAgo;
            Console.WriteLine(endpointWithMinskWoeidAnd5YearsAgoDate);

            var jsonWeatherTodayAnd5Days = GetJsonStringFromApi(endpointWithMinskWoeid);
            var weatherToday = JsonSerializer.Deserialize<WeatherRootObject>(jsonWeatherTodayAnd5Days).WheatherTodayAnd5DaysList[0];
            var jsonWeather5YearsAgo = GetJsonStringFromApi(endpointWithMinskWoeidAnd5YearsAgoDate);
            var weather5YearsAgoList = JsonSerializer.Deserialize<List<Weather>>(jsonWeather5YearsAgo);
            var weatherState5YearsAgoArray = weather5YearsAgoList.ConvertAll(weather => weather.WheatherStateName).ToArray();

            Assert.That(weatherState5YearsAgoArray, Contains.Item(weatherToday.WheatherStateName));
        }

        //helper classes and methods

        public class CityDataJsonObject
        {
            [JsonPropertyName("title")]
            public string City { get; set; }

            [JsonPropertyName("woeid")]
            public int Woeid { get; set; }

            [JsonPropertyName("latt_long")]
            public string LatLong { get; set; }
        }

        public class Weather
        {
            [JsonPropertyName("applicable_date")]
            public string Date { get; set; }

            [JsonPropertyName("min_temp")]
            public double Min_temp { get; set; }

            [JsonPropertyName("max_temp")]
            public double Max_temp { get; set; }

            [JsonPropertyName("the_temp")]
            public double The_temp { get; set; }

            [JsonPropertyName("weather_state_name")]
            public string WheatherStateName { get; set; }
        }

        public class WeatherRootObject
        {
            [JsonPropertyName("consolidated_weather")]
            public List<Weather> WheatherTodayAnd5DaysList { get; set; }
        }

        public CityDataJsonObject ExtractCityDataFromSearchResponse(string jsonString, string city)
        {
            var locationsList = JsonSerializer.Deserialize<List<CityDataJsonObject>>(jsonString);

            foreach (var location in locationsList)
            {
                if (location.City == city)
                {
                    return location;
                }
            }
            return null;
        }

        public string GetJsonStringFromApi(string url)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return jsonString;
        }
    }
}
