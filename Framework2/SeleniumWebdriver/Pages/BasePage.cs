using System;
using log4net;
using OpenQA.Selenium;

namespace SeleniumWebdriver.Pages
{
    public abstract class BasePage<T>
    {
        protected IWebDriver Driver;
        protected ILog Logger;

        protected BasePage(IWebDriver driver, ILog logger)
        {
            Driver = driver;
            Logger = logger;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(40);
        }

        public abstract T OpenPage();
    }
}
