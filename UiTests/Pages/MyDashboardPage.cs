using OpenQA.Selenium;

namespace BookingTests
{
    public class MyDashboardPage : WebPage
    {
        public MyDashboardPage()
        {
        }

        public bool IsMyDassboardPage()
        {
            return _driver.FindElement(By.TagName("body")).GetAttribute("class").Contains("mydashboard");
        }
    }
}
