using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingTests
{
    public class DriverSingleton
    {
        private DriverSingleton()
        {
        }

        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Window.Maximize();
            }
            return _driver;
        }

        public static void CloseDriver()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
