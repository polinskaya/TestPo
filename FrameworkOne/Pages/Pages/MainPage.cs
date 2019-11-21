using System;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages.Pages
{
    public class MainPage : BasePage<MainPage>
    {
        private const string SearchButtonSelector = "//BUTTON[@type='submit'][text()='НАЙТИ']";
        private const string DestinationFieldSelector = "//INPUT[@id='twidget-destination']";
        private const string FromFieldSelector = "//INPUT[@id='twidget-origin']";

        [FindsBy(How = How.XPath, Using = SearchButtonSelector)]
        public IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationFieldSelector)]
        public IWebElement DestinationField { get; set; }

        [FindsBy(How = How.XPath, Using = FromFieldSelector)]
        public IWebElement FromField { get; set; }


        public MainPage(IWebDriver driver, ILog logger) : base(driver, logger)
        {
            PageFactory.InitElements(driver, this);
        }

        public MainPage WriteIntoDestinationField(string text)
        {
            Logger.Info($"WriteIntoDestinationField: {text}.");
            DestinationField.Clear();
            DestinationField.SendKeys(text);
            return this;
        }

        public MainPage WriteIntoFromField(string text)
        {
            Logger.Info($"WriteIntoFromField: {text}.");
            FromField.SendKeys(Keys.Control +"A" +Keys.Backspace);
            FromField.SendKeys(text);
            return this;
        }

        public SearchResultPage ClickToSearchButton()
        {
            Logger.Info($"perform search of tickets.");
            SearchButton.Click();
            return new SearchResultPage(Driver, Logger);
        }

        public override MainPage OpenPage()
        {
            Logger.Info($"Open main page.");
            Driver.Navigate().GoToUrl("https://loukosterov.ru/");
            return this;
        }
    }
}
