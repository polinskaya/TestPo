using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebdriver.Tests
{
    public abstract class TestBase
    {
        protected IWebDriver _webDriver;

        [SetUp]
        public void StartBrowser()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(40);
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl("https://loukosterov.ru/");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _webDriver.Quit();
            _webDriver.Dispose();
        }

        protected IWebElement GetWebElement(string xPath)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }
    }
}
