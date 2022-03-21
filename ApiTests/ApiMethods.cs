using System.Net.Http;

namespace ApiTests
{
    public class ApiMethods
    {
        public const string BASE_URL = "https://www.metaweather.com/api/";

        public static string LocationSearch(string find)
        {
            if (find.Contains(","))
                return GetJson(BASE_URL + $"location/search/?lattlong={find}");

            return GetJson(BASE_URL + $"location/search/?query={find}");
        }

        public static string Location(int woeid)
        {
            return GetJson(BASE_URL + $"location/{woeid}");
        }

        public static string LocationDay(int woeid, string date)
        {
            return GetJson(BASE_URL + "location/" + woeid + "/" + date);
        }

        private static string GetJson(string url)
        {
            using (var _client = new HttpClient())
            {
                var response =
                    _client.GetAsync(url).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
