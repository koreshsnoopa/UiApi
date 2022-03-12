using OpenQA.Selenium;

namespace BookingTests
{
    public class MainPage : WebPage
    {
        By CurrencyButtonLocator = By.XPath("//button[@data-modal-header-async-type='currencyDesktop']");
        By CurrentCurrencyLocator = By.XPath("//button[@data-modal-header-async-type='currencyDesktop']/span/span[1]");
        By LanguageButtonLocator = By.XPath("//button[@data-modal-id='language-selection']");
        By FlightsPageLocator = By.XPath("//a[@data-decider-header='flights']");
        By FlagImgLocator = By.XPath("//img[contains(@src,'flags/new')]");
        By LogInButtonLocator = By.XPath("//a[contains(@class,'login')][@data-google-track]");
        By MenuLocator = By.XPath("//a[contains(@href,'mydashboard')]");
        By ReservationsLocator = By.XPath("//a[contains(@href,'myreservations')][@class!='manage']");
        By DashboardLocator = By.XPath("//a[@data-trackname='Dashboard']");
        By CityInputLocator = By.XPath("//input[@type='search']");
        By CheckInAdOutLocator = By.XPath("//div[@data-mode='checkin']");
        By ChangeNumerOfPeopleLocator = By.XPath("//div[@data-component='search/group/group-with-modal']");
        By SearchButtonLocator = By.XPath("//button[@data-sb-id='main']");
        By AddAgeOfKidLocator = By.XPath("//select[@name='age']");

        IWebElement _checkInAndOut;
        IWebElement _cityInput;
        IWebElement _flightsPage;
        IWebElement _curryncyButton;
        IWebElement _languageBytton;
        IWebElement _minusButton;
        IWebElement _plusButton;
        IWebElement _changeNumberOfPeople;
        IWebElement _searchButton;

        public MainPage()
        {
            _curryncyButton = _driver.FindElement(CurrencyButtonLocator);
            _languageBytton = _driver.FindElement(LanguageButtonLocator);
            _flightsPage = _driver.FindElement(FlightsPageLocator);
            _cityInput = _driver.FindElement(CityInputLocator);
            _checkInAndOut = _driver.FindElement(CheckInAdOutLocator);
            _changeNumberOfPeople = _driver.FindElement(ChangeNumerOfPeopleLocator);
            _searchButton = _driver.FindElement(SearchButtonLocator);
        }

        //Currency in short form e.g. BYN, RUB, PLN etc.
        public MainPage ChangeCurrency(string currency)
        {
            _curryncyButton.Click();
            _driver.FindElement(By.XPath($"//ul[contains(@class,'size')]//a[contains(@href,'{currency}')]/.."))
                .Click();
            return this;
        }

        public MainPage ChangeLanguage(string language)
        {
            _languageBytton.Click();
            _driver.FindElement(By.XPath($"//div[@lang][contains(text(),'{language}')]")).Click();
            return this;
        }

        public FlightsPage GoToFlightsPage()
        {
            string url1 = _driver.Url;
            _flightsPage.Click();
            _driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(0);
            wait.Until(d => d.FindElement(By.XPath("//body[contains(@class, 'FlightsSearch')]")));
            _driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);
            return new FlightsPage();
        }

        public MainPage LogInAs(User user)
        {
            _driver.FindElement(LogInButtonLocator).Click();
            return new LogInPage().LogInAs(user);
        }

        public MyDashboardPage GoToMyDashboard()
        {
            _driver.FindElement(MenuLocator).Click();
            _driver.FindElement(ReservationsLocator).Click();
            _driver.FindElement(DashboardLocator).Click();
            return new MyDashboardPage();
        }

        public string GetCurrentCurrency()
        {
            return _driver.FindElement(CurrentCurrencyLocator).Text.Trim();
        }

        public string GetTodaysDate()
        {
            return _driver.FindElement(By.XPath("//td[contains(@class,'today')]")).GetAttribute("data-date");
        }

        public string GetNDaysFromTodayDate(int n)
        {
           string todayDate = GetTodaysDate(); 
           int.TryParse(todayDate.Remove(0, todayDate.Length - 2), out int res);
            return todayDate.Remove(todayDate.Length - 2)+(res + n).ToString();
        }

        private void SetNumberOfPeopleAndRooms(ItemsToFilter item, int number) 
        {
            _changeNumberOfPeople.Click();
            int.TryParse(_driver
               .FindElement(By.XPath($"//button[contains(@aria-describedby,'{item}')][1]/following-sibling::span[1]")).Text,
               out int res);
            if (res == number)
            {
                _changeNumberOfPeople.Click();
                return;
            }
            _minusButton = _driver.FindElement(By.XPath($"//button[contains(@aria-describedby,'{item}')][1]"));
            _plusButton = _driver.FindElement(By.XPath($"//button[contains(@aria-describedby,'{item}')][2]"));
            if (res > number)
            {
                while (res != number)
                {
                    _minusButton.Click(); 
                    res--;
                }
            }
            if (res < number)
            {
                while (res != number)
                {
                    _plusButton.Click();
                    res++;
                }
            }
        }

        public SearchResultPage SetFilterAndSearch(string checkInDate, string checkOutDate, int numberOfAdults,
            int numberOfRooms, string city) 
        {
            _checkInAndOut.Click();
            _driver.FindElement(By.XPath($"//td[@data-date='{checkInDate}']")).Click();
            _driver.FindElement(By.XPath($"//td[@data-date='{checkOutDate}']")).Click();
            _cityInput.SendKeys(city);
            _driver.FindElement(By.XPath("//ul[@role='listbox']/li[1]")).Click();
            SetNumberOfPeopleAndRooms(ItemsToFilter.adults, numberOfAdults);
            SetNumberOfPeopleAndRooms(ItemsToFilter.rooms, numberOfRooms);
            _searchButton.Click();
            return new SearchResultPage();
        }

        public SearchResultPage SetFilterAndSearch(string checkInDate, string checkOutDate, int numberOfAdults,
            int numberOfKids, int ageOfKid, int numberOfRooms, string city)
        {
            SetNumberOfPeopleAndRooms(ItemsToFilter.children, numberOfKids);
            _driver.FindElement(AddAgeOfKidLocator).Click();
            _driver.FindElement(By.XPath($"//option[@value='{ageOfKid}']")).Click();
            SetFilterAndSearch(checkInDate, checkOutDate, numberOfAdults, numberOfRooms, city);
            return new SearchResultPage();
        }

        public string GetCurrentLanguage()
        {
            string key = _driver.FindElement(FlagImgLocator).GetAttribute("src");
            string temp = key.Remove(key.LastIndexOf('/'));
            return _driver.FindElement(By.XPath($"//link[@rel='alternate'][contains(@hreflang,'{temp.Remove(0, temp.LastIndexOf('/') + 1)}')]"))
                .GetAttribute("title");
        }
    }
}
