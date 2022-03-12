using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BookingTests
{
    public abstract class WebPage
    {
        protected IWebDriver _driver;
        protected WebDriverWait wait;

        public WebPage()
        {
            _driver = DriverSingleton.GetDriver();
            wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(10));

        }
    }
}
