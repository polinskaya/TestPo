using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumWebdriver.Pages
{
    public class MainPage : BasePage<MainPage>
    {
        private const string SearchButtonSelector = "//BUTTON[@type='submit'][text()='НАЙТИ']";
        private const string DestinationFieldSelector = "//INPUT[@id='twidget-destination']";
        private const string FromFieldSelector = "//INPUT[@id='twidget-origin']";

        private const string PassengersControlSelector = "//DIV[@class='twidget-passengers-detail']";
        private const string BusinessClassCheckBoxSelector = "//LABEL[text()='Бизнес-класс']";
        private const string ElderPassengersIncrementControlSelector = "(//SPAN[@class='twidget-inc twidget-q-btn'])[1]";
        private const string ApplyPassengersCountSelector = "//DIV[@class='twidget-passengers-ready-button']";
        private const string PassengersCountTextSelector = "//DIV[@class='twidget-pas-no']";

        private const string DepartCalendarSelector = "//SPAN[contains(@class,'twidget-date-depart')]";
        private const string ReturnCalendarSelector = "//SPAN[contains(@class,'twidget-date-return')]";
        private const string CalendarNextMonthSelector = "(//TH[@class='next'])[1]";
        private const string CalendarPrevMonthSelector = "(//TH[@class='prev'])[1]";
        private const string CurrentMonthSelector = "(//TH[@colspan='5'])[1]";

        private const string CancelDepartDateButtonSelector = "//TD[@class='datepicker-cancel-return-date']";

        private const string FirstAviaCompanyControlSelector = "//IMG[@class='aligncenter size-full wp-image-10556']";

        private const string SelectCalendarDatesFormat = "//TD[contains(@class,'day') and not(contains(@class, 'disabled'))]";

        [FindsBy(How = How.XPath, Using = SearchButtonSelector)]
        public IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationFieldSelector)]
        public IWebElement DestinationField { get; set; }

        [FindsBy(How = How.XPath, Using = FromFieldSelector)]
        public IWebElement FromField { get; set; }

        [FindsBy(How = How.XPath, Using = PassengersControlSelector)]
        public IWebElement PassengersControl { get; set; }

        [FindsBy(How = How.XPath, Using = ElderPassengersIncrementControlSelector)]
        public IWebElement ElderPassengersIncrementControl { get; set; }

        [FindsBy(How = How.XPath, Using = ApplyPassengersCountSelector)]
        public IWebElement ApplyPassengersButton { get; set; }

        [FindsBy(How = How.XPath, Using = PassengersCountTextSelector)]
        public IWebElement PassengersCountText { get; set; }

        [FindsBy(How = How.XPath, Using = DepartCalendarSelector)]
        public IWebElement DepartCalendar { get; set; }

        [FindsBy(How = How.XPath, Using = ReturnCalendarSelector)]
        public IWebElement ReturnCalendar { get; set; }

        [FindsBy(How = How.XPath, Using = CalendarNextMonthSelector)]
        public IWebElement CalendarNextMonthButton { get; set; }

        [FindsBy(How = How.XPath, Using = CalendarPrevMonthSelector)]
        public IWebElement CalendarPrevMonthButton { get; set; }

        [FindsBy(How = How.XPath, Using = CurrentMonthSelector)]
        public IWebElement CalendarCurrentMonth { get; set; }

        [FindsBy(How = How.XPath, Using = BusinessClassCheckBoxSelector)]
        public IWebElement BusinessClassCheckBox { get; set; }

        [FindsBy(How = How.XPath, Using = CancelDepartDateButtonSelector)]
        public IWebElement CancelDepartDateButton { get; set; }

        [FindsBy(How = How.XPath, Using = FirstAviaCompanyControlSelector)]
        public IWebElement FirstAviaCompany { get; set; }

        private Dictionary<int, string> IntToMonth = new Dictionary<int, string>
        {
            {1,"Январь" },
            {2,"Февраль" },
            {3,"Март" },
            {4,"Апрель" },
            {5,"Май" },
            {6,"Июнь" },
            {7,"Июль" },
            {8,"Август" },
            {9,"Сентябрь" },
            {10,"Октябрь" },
            {11,"Ноябрь" },
            {12,"Декабрь" }
        };

        private Dictionary<string, int> MonthToInt = new Dictionary<string, int>
        {
            {"Январь",1 },
            {"Февраль",2 },
            {"Март",3 },
            {"Апрель",4 },
            {"Май",5 },
            {"Июнь",6 },
            {"Июль" ,7},
            {"Август" ,8},
            {"Сентябрь" ,9},
            {"Октябрь",10 },
            {"Ноябрь",11 },
            {"Декабрь",12 }
        };


        public MainPage(IWebDriver driver, ILog logger) : base(driver, logger)
        {
            PageFactory.InitElements(driver, this);
        }

        public MainPage WriteIntoDestinationField(string text)
        {
            LogInfo(nameof(MainPage), $" Write Into Destination Field: {text}.");
            DestinationField.Clear();
            DestinationField.SendKeys(text);
            return this;
        }

        public MainPage WriteIntoFromField(string text)
        {
            LogInfo(nameof(MainPage), $"Write Into From Field: {text}.");
            FromField.SendKeys(Keys.Control + "A" + Keys.Backspace);
            FromField.SendKeys(text);
            return this;
        }

        public SearchResultPage ClickToSearchButton()
        {
            LogInfo(nameof(MainPage), $"perform search of tickets.");
            SearchButton.Click();
            return new SearchResultPage(Driver, Logger);
        }

        public AviaCompanyPage ClickToAviacompanyButton()
        {
            LogInfo(nameof(MainPage), $"select aviacompany.");
            FirstAviaCompany.Click();
            return new AviaCompanyPage(Driver, Logger);
        }

        public MainPage OpenPassengersControl()
        {
            LogInfo(nameof(MainPage), $"open passengers selection form.");
            PassengersControl.Click();
            return this;
        }

        public MainPage AddElderPassengers(int passengersCount)
        {
            LogInfo(nameof(MainPage), $"Add {passengersCount} elder passengers.");
            for (int i = 0; i < passengersCount; i++)
            {
                ElderPassengersIncrementControl.Click();
            }

            return this;
        }

        public MainPage ApplyPassengers()
        {
            LogInfo(nameof(MainPage), $" Apply passengers count.");
            ApplyPassengersButton.Click();
            return this;
        }

        public string GetPassengersCount()
        {
            return PassengersCountText.Text;
        }

        public MainPage OpenDepartCalendar()
        {
            LogInfo(nameof(MainPage), $"open depart calendar.");
            DepartCalendar.Click();
            return this;
        }

        public MainPage OpenReturnCalendar()
        {
            LogInfo(nameof(MainPage), $"open return calendar.");
            ReturnCalendar.Click();
            return this;
        }

        public MainPage SelectDepartDate(double? dayOffset = null)
        {
            DateTime selectedDate = GetDate(dayOffset);

            int currentSelectedMonthIndex = GetCurrentSelectedToMonthIndex();
            int currentMonth = selectedDate.Month;

            //выбор текущего месяца
            if (currentSelectedMonthIndex != currentMonth)
            {
                if (currentMonth > currentSelectedMonthIndex)
                {
                    SelectNextMonth();
                }

                if (currentMonth < currentSelectedMonthIndex)
                {
                    SelectPreviousMonth();
                }

                currentSelectedMonthIndex = GetCurrentSelectedToMonthIndex();
                if (currentSelectedMonthIndex != currentMonth)
                {
                    LogInfo(nameof(MainPage), $"{currentMonth} could not be find in calendar control.");
                    throw new InvalidOperationException($"{currentMonth} could not be find in calendar control.");
                }
            }

            SelectDate(selectedDate);

            return this;
        }

        public MainPage SelectReturnDate(double? dayOffset = null)
        {
            DateTime selectedDate = GetDate(dayOffset);

            int currentSelectedMonthIndex = GetCurrentSelectedToMonthIndex();
            int currentMonth = selectedDate.Month;

            //выбор текущего месяца
            if (currentSelectedMonthIndex != currentMonth)
            {
                if (currentMonth > currentSelectedMonthIndex)
                {
                    SelectNextMonth();
                }

                if (currentMonth < currentSelectedMonthIndex)
                {
                    SelectPreviousMonth();
                }

                currentSelectedMonthIndex = GetCurrentSelectedToMonthIndex();
                if (currentSelectedMonthIndex != currentMonth)
                {
                    LogInfo(nameof(MainPage), $"{currentMonth} could not be find in calendar control.");
                    throw new InvalidOperationException($"{currentMonth} could not be find in calendar control.");
                }
            }

            SelectDate(selectedDate);

            return this;
        }

        private void SelectDate(DateTime selectedDate)
        {
            List<IWebElement> availableDates = Driver.FindElements(By.XPath(SelectCalendarDatesFormat)).ToList();
            bool dateFound = false;
            foreach (IWebElement availableDate in availableDates)
            {
                if (availableDate.Text.Equals(selectedDate.Day.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCulture))
                {
                    availableDate.Click();
                    dateFound = true;
                    LogInfo(nameof(MainPage), $"{selectedDate} date selected.");
                    break;
                }
            }

            if (!dateFound)
            {
                throw new InvalidOperationException($"{selectedDate.Day} could not be find in calendar control.");
            }
        }

        private DateTime GetDate(double? offset)
        {

            if (offset == null)
            {
                return DateTime.Today;
            }

            DateTime selectedDate = DateTime.Today;
            double countOfDaysInCurrentMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
            double daysWithOffset = selectedDate.Day + offset.Value;
            if (daysWithOffset > countOfDaysInCurrentMonth)
            {
                selectedDate = selectedDate.AddMonths(1);
                double newDay = daysWithOffset - countOfDaysInCurrentMonth;
                double diff = selectedDate.Day - newDay;
                if (diff > 0)
                {
                    selectedDate = selectedDate.AddDays(-diff);
                }
                else
                {
                    selectedDate = selectedDate.AddDays(diff);
                }
            }

            return selectedDate.AddDays(offset.Value);
        }

        private int GetCurrentSelectedToMonthIndex()
        {
            string currentSelectedMonth = CalendarCurrentMonth.Text.Split(' ')[0].Trim();
            return MonthToInt[currentSelectedMonth];
        }

        private MainPage SelectPreviousMonth()
        {
            LogInfo(nameof(MainPage), $"navigate to prev month.");
            CalendarPrevMonthButton.Click();
            return this;
        }

        private MainPage SelectNextMonth()
        {
            LogInfo(nameof(MainPage), $"navigate to next month.");
            CalendarNextMonthButton.Click();
            return this;
        }

        public MainPage SelectBusinessClassTicketType()
        {
            LogInfo(nameof(MainPage), $"select business class tickets.");
            BusinessClassCheckBox.Click();
            return this;
        }

        public MainPage CancelDepartDate()
        {
            LogInfo(nameof(MainPage), $"cancel depart date.");
            CancelDepartDateButton.Click();
            return this;
        }


        public override MainPage OpenPage()
        {
            LogInfo(nameof(MainPage), $"Open main page.");
            Driver.Navigate().GoToUrl("https://loukosterov.ru/");
            return this;
        }


    }
}
