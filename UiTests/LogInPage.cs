using OpenQA.Selenium;

namespace BookingTests
{
    public class LogInPage : WebPage
    {
        public LogInPage()
        {
        }

        public MainPage LogInAs(User user)
        {
            _driver.FindElement(By.XPath("//input[@type='email']")).SendKeys(user.Username);
            _driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            _driver.FindElement(By.XPath("//input[@type='password'][@data-focus]")).SendKeys(user.Password);
            _driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            return new MainPage();
        }
    }
}
