using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumWebdriver.Tests
{
    
    public abstract class TestBase
    {
        protected IWebDriver _webDriver;

        [OneTimeSetUp]
        public void InitLogger()
        {
            LoggerConfiguration.InitLogger();
        }

        [SetUp]
        public void StartBrowser()
        {
            _webDriver = WebDriverManagerInternal.WebDriverManagerInternal.SetUpWebDriver();
        }

        protected void TestWrapper(Action testAction, string testName)
        {
            try
            {
                LoggerConfiguration.Log.Info($"Test: {testName}, started.");
                testAction();
                LoggerConfiguration.Log.Info($"Test: {testName}, passed.");
            }
            catch(Exception exception)
            {
                LoggerConfiguration.Log.Error($"Test {testName} fails.", exception);
                var screenshotsFolder = $"{AppDomain.CurrentDomain.BaseDirectory}{@"\Screenshots"}";
                Directory.CreateDirectory(screenshotsFolder);
                var screenShot = _webDriver.TakeScreenshot();
                screenShot.SaveAsFile(
                    $"{screenshotsFolder}{@"\screenshot"}{DateTime.Now.ToString("yy-MM-dd_hh-mm-ss")}.png",
                    ScreenshotImageFormat.Png);
                throw;
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            WebDriverManagerInternal.WebDriverManagerInternal.CloseDriver();
        }
    }
}
