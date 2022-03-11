using OpenQA.Selenium;

namespace BookingTests
{
    public class FlightsPage : WebPage
    {
        public FlightsPage()
        {
        }

        public bool IsFlightPage()
        {
            return _driver.FindElement(By.TagName("html")).GetAttribute("class").Contains("Flights");
        }
    }
}
