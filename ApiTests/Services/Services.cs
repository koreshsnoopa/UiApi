using System;
using System.Net.Http;

namespace ApiTests
{
    public class Services
    {
        public const int MAX_TEMP_SUMMER = 40;
        public const int MAX_TEMP_AUTUMN = 25;
        public const int MAX_TEMP_SPRING = 28;
        public const int MAX_TEMP_WINTER = 7;
        public const int MIN_TEMP_SUMMER = 5;
        public const int MIN_TEMP_AUTUMN = -15;
        public const int MIN_TEMP_SPRING = -15;
        public const int MIN_TEMP_WINTER = -30;

        public static Seasons GetCurrentSeason()
        {
            int.TryParse(DateTime.Now.ToString("MM"), out int mounth);

            if (mounth >= 3 && mounth <= 5)
                return Seasons.Spring;
            if (mounth >= 9 && mounth <= 11)
                return Seasons.Autumn;
            if (mounth >= 6 && mounth <= 8)
                return Seasons.Summer;

            return Seasons.Winter;
        }

        public static bool CheckTemperature(Seasons season, double min, double max)
        {
            if (season.Equals(Seasons.Spring) && max <= MAX_TEMP_SPRING && min >= MIN_TEMP_SPRING)
                return true;
            if (season.Equals(Seasons.Summer) && max <= MAX_TEMP_SUMMER && min >= MIN_TEMP_SUMMER)
                return true;
            if (season.Equals(Seasons.Winter) && max <= MAX_TEMP_WINTER && min >= MIN_TEMP_WINTER)
                return true;
            if (season.Equals(Seasons.Autumn) && max <= MAX_TEMP_AUTUMN && min >= MIN_TEMP_AUTUMN)
                return true;

            return false;
        }
    }
}
