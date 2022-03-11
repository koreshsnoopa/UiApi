using OpenQA.Selenium;

namespace BookingTests
{
    public abstract class WebPage
    {
        protected IWebDriver _driver;

        public WebPage()
        {
            _driver = DriverSingleton.GetDriver();
        }
    }
}
