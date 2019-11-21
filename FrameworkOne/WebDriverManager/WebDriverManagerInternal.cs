using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebDriverManagerInternal
{
    public static class WebDriverManagerInternal
    {
        private static IWebDriver _webDriver;

        public static IWebDriver SetUpWebDriver()
        {
            if (_webDriver == null)
            {
                switch (TestContext.Parameters.Get("browser"))
                {
                    case "Chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        _webDriver = new ChromeDriver();
                        break;
                    default:
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        _webDriver = new ChromeDriver();
                        break;

                }

                _webDriver.Manage().Window.Maximize();
            }

            return _webDriver;
        }

        public static void CloseDriver()
        {
            _webDriver.Quit();
            _webDriver = null;
        }
    }
}
