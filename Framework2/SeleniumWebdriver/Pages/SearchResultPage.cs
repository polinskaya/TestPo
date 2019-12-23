using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebdriver.Pages
{
    public class SearchResultPage : BasePage<SearchResultPage>
    {
        private const string ErrorMessageSelector = "//DIV[@class='description description--bad_search_params']";
        private const string DestinationFieldSelector = "//INPUT[@id='flights-destination-prepop-whitelabel_ru']";

        private const string FromPlaceSelector = "//span[contains(@class,'mewtwo-flights-origin-country__pseudo')]";
        private const string AgencyOptionSelectorFormat = "//LABEL[@class='label-block name airlines-label g-text-overflow' and contains(text(),'{0}')]";
        private const string AgencySelector = "(//div[contains(@class,'title-dropdown')])[10]";
        private const string AllAgencyOptionSelector = "(//LABEL[@class='label-block name stops-label'][text()='Все'][text()='Все'])[5]";
        private const string AgencyOnTicketSelector = "(//DIV[@class='ticket-action__main_proposal ticket-action__main_proposal--'])[1]";
        private const string DestinationPlaceSelector = "//span[contains(@class,'mewtwo-flights-destination-country__pseudo')]";

        private const string FirstTicketTimeHoursSelector = "(//HEADER[@class='flight-brief-layovers__flight_time'])[7]/span[@class='formatted_time'][1]";
        private const string FirstTicketTimeMinutesSelector = "(//HEADER[@class='flight-brief-layovers__flight_time'])[7]/span[@class='formatted_time'][2]";

        private const string SecondTicketTimeHoursSelector = "(//HEADER[@class='flight-brief-layovers__flight_time'])[9]/span[@class='formatted_time'][1]";
        private const string SecondTicketTimeMinutesSelector = "(//HEADER[@class='flight-brief-layovers__flight_time'])[9]/span[@class='formatted_time'][2]";

        private const string SortControlSelector = "//DIV[@class='custom_select__selected']";
        private const string SortByTimeOptionSelector = "//LI[@data-option-id='duration']";

        private const string SelectReturnPartsFormat = "//DIV[@class='flight flight--return']";


        [FindsBy(How = How.XPath, Using = ErrorMessageSelector)]
        public IWebElement ErrorMessage { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationFieldSelector)]
        public IWebElement DestinationField { get; set; }

        [FindsBy(How = How.XPath, Using = FromPlaceSelector)]
        public IWebElement FromPlace { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationPlaceSelector)]
        public IWebElement DestinationPlace { get; set; }

        [FindsBy(How = How.XPath, Using = SortControlSelector)]
        public IWebElement SortControl { get; set; }

        [FindsBy(How = How.XPath, Using = SortByTimeOptionSelector)]
        public IWebElement SortByTimeOption { get; set; }

        [FindsBy(How = How.XPath, Using = FirstTicketTimeHoursSelector)]
        public IWebElement FirstTicketHours { get; set; }

        [FindsBy(How = How.XPath, Using = FirstTicketTimeMinutesSelector)]
        public IWebElement FirstTicketMinutes { get; set; }

        [FindsBy(How = How.XPath, Using = SecondTicketTimeHoursSelector)]
        public IWebElement SecondTicketHours { get; set; }

        [FindsBy(How = How.XPath, Using = SecondTicketTimeMinutesSelector)]
        public IWebElement SecondTicketMinutes { get; set; }

        [FindsBy(How = How.XPath, Using = AgencySelector)]
        public IWebElement AgencyControl { get; set; }

        [FindsBy(How = How.XPath, Using = AllAgencyOptionSelector)]
        public IWebElement AllAgencyOption { get; set; }

        [FindsBy(How = How.XPath, Using = AgencyOnTicketSelector)]
        public IWebElement AgencyOnTicket{ get; set; }

        public SearchResultPage(IWebDriver driver, ILog logger):base(driver, logger)
        {
            PageFactory.InitElements(driver, this);
        }

        public override SearchResultPage OpenPage()
        {
            LogInfo(nameof(SearchResultPage), $"Open search result page.");
            return this;
        }

        public SearchResultPage OpenSortOptionList()
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0,0,0,30));
            LogInfo(nameof(SearchResultPage), $"Open sort options list.");
            SortControl.Click();
            return this;
        }

        public SearchResultPage OpenAgencyOptionList()
        {
            LogInfo(nameof(SearchResultPage), $"Open agency options list.");
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 0, 30));
            AdjustElementExibition(AgencyControl, 1000);
            var actions = new Actions(this.Driver);
            actions.MoveToElement(AgencyControl);
            actions.Click(AgencyControl);
            actions.Perform();
            return this;
        }

        private void AdjustElementExibition(IWebElement elemento, int scrollValue)
        {
            if (elemento.Location.Y < 400) return;

            ExecutarComandoJavascript($"window.scrollBy(0,{scrollValue})");
            Thread.Sleep(100); //sometimes js take some miliseconds to execute;
        }

        private object ExecutarComandoJavascript(string script)
        {
            return ((IJavaScriptExecutor)this.Driver).ExecuteScript(script);
        }

        public SearchResultPage SelectSortByTimeOption()
        {
            LogInfo(nameof(SearchResultPage), $"select sort by time option.");
            SortByTimeOption.Click();
            return this;
        }

        public SearchResultPage SelectAllAgencyOption()
        {
            LogInfo(nameof(SearchResultPage), $"select all agency option.");
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 0, 30));
            var actions = new Actions(this.Driver);
            actions.MoveToElement(AllAgencyOption);
            actions.Click(AllAgencyOption);
            actions.Perform();

            return this;
        }

        public SearchResultPage SelectAgencyOption(string agency)
        {
            LogInfo(nameof(SearchResultPage), $"select {agency} option.");
            var selector = string.Format(AgencyOptionSelectorFormat, agency);

            var agencyOption = Driver.FindElement(By.XPath(selector));

            agencyOption.Click();
            return this;
        }

        public bool AllTicketsWithoutDepartPart()
        {
            List<IWebElement> availableDates = Driver.FindElements(By.XPath(SelectReturnPartsFormat)).ToList();
            return !availableDates.Any(element=> element.Displayed);
        }

        public DateTime GetFirstTicketTime()
        {
            var dateTime = DateTime.MinValue;
            string hours = Regex.Replace(FirstTicketHours.Text, @"[^\d]", "");
            dateTime = dateTime.AddHours(double.Parse(hours));

            string minutes = Regex.Replace(FirstTicketMinutes.Text, @"[^\d]", "");
            dateTime = dateTime.AddMinutes(double.Parse(minutes));
            return dateTime;
        }

        public DateTime GetSecondTicketTime()
        {
            var dateTime = DateTime.MinValue;
            string hours = Regex.Replace(SecondTicketHours.Text, @"[^\d]", "");
            dateTime = dateTime.AddHours(double.Parse(hours));

            string minutes = Regex.Replace(SecondTicketMinutes.Text, @"[^\d]", "");
            dateTime = dateTime.AddMinutes(double.Parse(minutes));
            return dateTime;
        }
    }
}
