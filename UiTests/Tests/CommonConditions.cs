using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace BookingTests
{
    public class CommonConditions
    {
        protected IWebDriver driver;
        public const string BOOKING_URL = "https://www.booking.com/";

        [SetUp]
        public void SetUp()
        {
            driver = DriverSingleton.GetDriver();
        }

        [TearDown]
        public void StopBrowser()
        {
            var result = TestContext.CurrentContext.Result.Outcome;
            if (result.Equals(ResultState.Failure) || result.Equals(ResultState.Error))
            {
                var screenFile = ((ITakesScreenshot)DriverSingleton.GetDriver()).GetScreenshot();
                screenFile
                    .SaveAsFile($"/Users/marialukasova/Projects/UIApiTests/UiTests/Targets/{DateTime.Now.ToString("dd_MM_yy_HH_mm_ss")}.png", ScreenshotImageFormat.Png);
            }
            DriverSingleton.CloseDriver();
        }

    }
}
