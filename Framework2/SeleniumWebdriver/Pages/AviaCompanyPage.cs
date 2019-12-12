using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumWebdriver.Pages
{
    public class AviaCompanyPage : BasePage<AviaCompanyPage>
    {

        private const string TitleControlSelector = "(//DIV[@class='entry clearfix']/H2)[1]";
        [FindsBy(How = How.XPath, Using = TitleControlSelector)]
        public IWebElement AviaCompanyTitle { get; set; }

        public AviaCompanyPage(IWebDriver driver, ILog logger) : base(driver, logger)
        {
            PageFactory.InitElements(driver, this);
        }

        public override AviaCompanyPage OpenPage()
        {
            Logger.Info($"Class:{nameof(AviaCompanyPage)}, Open aviacompany page.");
            Driver.Navigate().GoToUrl("https://loukosterov.ru/spisok-loukosterov/loukoster-pobeda");
            return this;
        }
    }
}
