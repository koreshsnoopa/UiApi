using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ApiTests
{
    [TestFixture]
    public class Tests 
    {
        public const string REAL_LATTLONG = "53.90255,27.563101";
        public LocationInfo City;
        public ConsolidatedWeather weatherForToday;

        [Test, Order(1)]
        [TestCase("min")]
        public void SearchInfoForMinskTest(string queryEquals)
        {
           var result = ApiMethods.LocationSearch(queryEquals);
           City = JsonConvert.DeserializeObject<List<LocationInfo>>(result)
                .Where(x => x.Title.Equals("Minsk")).ToList()[0];

           Assert.IsNotNull(City);
        }

        [Test, Order(2)]
        public void LattLongIsCorrectTest()
        {
            var result = ApiMethods.LocationSearch("min");
            City = JsonConvert.DeserializeObject<List<LocationInfo>>(result)
                 .Where(x => x.Title.Equals("Minsk")).ToList()[0];

            Assert.AreEqual(REAL_LATTLONG, City.Latt_Long);
        }
        [Test, Order(3)]
        public void GetWeatherTest()
        {
           string result = ApiMethods.Location(City.Woeid);

           var weather = JsonConvert.DeserializeObject<WeatherRequest>(result);
            
           weatherForToday = weather.consolidated_weather
                    .Where(x => x.applicable_date.Equals(System.DateTime.Now.ToString("yyyy-MM-dd"))).ToList()[0];
           Assert.NotNull(weatherForToday);
        }

        [Test, Order(4)]
        public void TemperatureIsCorrectTest()
        {
            Assert.IsTrue(Services
                .CheckTemperature(Services.GetCurrentSeason(), weatherForToday.min_temp, weatherForToday.max_temp));
        }

        [Test, Order(5)]
        public void GetToday5YearsAgo()
        {
            int.TryParse(System.DateTime.Now.ToString("yyyy"), out int todaysYear);

            var result = ApiMethods.LocationDay(City.Woeid, (todaysYear - 5) + System.DateTime.Now.ToString("/M/d/"));
            var weathers = JsonConvert.DeserializeObject<List<ConsolidatedWeather>>(result);

            Assert.IsTrue(weathers.Count >= 1);
        }
    }
}
