using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Pages.Pages
{
    public class MainPage : BasePage
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

        private MainPage()
        {
        }

        public MainPage(IWebDriver driver)
        {
            Driver = driver;
            InitPage(driver, this);
        }

        public MainPage WriteIntoDestinationField(string text)
        {
            DestinationField.Clear();
            DestinationField.SendKeys(text);
            return new MainPage(Driver);
        }

        public MainPage WriteIntoFromField(string text)
        {
            FromField.SendKeys(Keys.Control +"A" +Keys.Backspace);
            FromField.SendKeys(text);
            return new MainPage(Driver);
        }

        public SearchResultPage ClickToSearchButton()
        {
            SearchButton.Click();
            return new SearchResultPage(Driver);
        }
    }
}
