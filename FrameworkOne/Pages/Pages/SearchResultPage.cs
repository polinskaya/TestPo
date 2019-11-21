using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages.Pages
{
    public class SearchResultPage : BasePage<SearchResultPage>
    {
        private const string ErrorMessageSelector = "//DIV[@class='description description--bad_search_params']";
        private const string DestinationFieldSelector = "//INPUT[@id='flights-destination-prepop-whitelabel_ru']";

        [FindsBy(How = How.XPath, Using = ErrorMessageSelector)]
        public IWebElement ErrorMessage { get; set; }

        [FindsBy(How = How.XPath, Using = DestinationFieldSelector)]
        public IWebElement DestinationField { get; set; }


        public SearchResultPage(IWebDriver driver, ILog logger):base(driver, logger)
        {
            PageFactory.InitElements(driver, this);
        }

        public override SearchResultPage OpenPage()
        {
            Logger.Info($"Open search result page.");
            return this;
        }
    }
}
