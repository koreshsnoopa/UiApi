using NUnit.Framework;

namespace BookingTests
{
    [TestFixture]
    public class Tests : CommonConditions
    {
        const string CURRENCY = "DKK";
        const string LANGUAGE = "English (US)";

       [Test]
        public void ChangeLanguageTest()
        {
            driver.Navigate().GoToUrl(BOOKING_URL);
            MainPage mainPage = new MainPage().ChangeLanguage(LANGUAGE);

            Assert.AreEqual(LANGUAGE, mainPage.GetCurrentLanguage());
        }

        [Test] 
        public void ChangeCurrencyTest()
        {
            driver.Navigate().GoToUrl(BOOKING_URL);
            MainPage mainPage = new MainPage().ChangeCurrency(CURRENCY);

            Assert.AreEqual(CURRENCY, mainPage.GetCurrentCurrency());
        }

        [Test]
        public void FlightsPageAcssesTest()
        {
            driver.Navigate().GoToUrl(BOOKING_URL);

            Assert.NotNull(new MainPage().GoToFlightsPage());
        }

        [Test]
        public void MyDashboardAcssesTest()
        {
            driver.Navigate().GoToUrl(BOOKING_URL);

            Assert.IsTrue( new MainPage().LogInAs(UserCreator.WithCredentialsFromProperty())
                .GoToMyDashboard().IsMyDassboardPage());
        }

        [Test]
        public void SetFilterTest()
        {
            driver.Navigate().GoToUrl(BOOKING_URL);
            MainPage mainPage = new MainPage();
            mainPage.SetFilterAndSearch(mainPage.GetTodaysDate(), mainPage.GetNDaysFromTodayDate(2), 2, 1, 5, 1, "Paris");
            Assert.Pass();
        }
    }
}
