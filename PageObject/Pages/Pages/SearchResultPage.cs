using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages.Pages
{
    public class SearchResultPage : BasePage
    {
        private const string ErrorMessageSelector = "//DIV[@class='description description--bad_search_params']";
        private const string DestinationFieldSelector = "//INPUT[@id='flights-destination-prepop-whitelabel_ru']";

        [FindsBy(How = How.XPath, Using = ErrorMessageSelector)]
        public IWebElement ErrorMessage { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationFieldSelector)]
        public IWebElement DestinationField { get; set; }

        private SearchResultPage()
        {
        }

        public SearchResultPage(IWebDriver driver)
        {
            Driver = driver;
            InitPage(driver, this);
        }
    }
}
