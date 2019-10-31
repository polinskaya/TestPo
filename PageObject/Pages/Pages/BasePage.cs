using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;

        protected void InitPage<T>(IWebDriver driver, T page) where T :BasePage
        {
            PageFactory.InitElements(driver, page);
        }
    }
}
