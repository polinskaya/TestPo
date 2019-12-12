using NUnit.Framework;
using Pages.Pages;
using SeleniumWebdriver.Models;
using SeleniumWebdriver.Services;

namespace SeleniumWebdriver.Tests
{
    [TestFixture]
    public class SearchTest : TestBase
    {
        private const string ExpectedErrorText =
            "Введённые вами параметры поиска некорректны.\r\nПожалуйста, измените параметры поиска и запустите новый поиск.";
        /*
        7. Ввод одинакового города вылета и прибытия
        Шаги:
        Зайти на главную страницу;
        заполнить формы: Город вылета (Минск),
        Город прибытия (Минск),
        нажать кнопку "Найти".
        */
        [Test]
        public void SearchWithEqualDepartureAndDestinationPlace()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModelWithEqualFromAndDestinationProps();
                MainPage mainPage = new MainPage(_webDriver, LoggerConfiguration.Log);
                var searchResultPage =
                    mainPage
                        .OpenPage()
                        .WriteIntoFromField(requestModel.From)
                        .WriteIntoDestinationField(requestModel.To)
                        .ClickToSearchButton();

                Assert.AreEqual(ExpectedErrorText, searchResultPage.ErrorMessage.Text);
            }, nameof(SearchWithEqualDepartureAndDestinationPlace));
        }


        /*
           2. Поиск возможного маршрута без ввода Города прибытия
           Шаги:
           Зайти на главную страницу;
           заполнить формы: Город вылета (Минск),Город прибытия (3212312312),
           нажать кнопку "Найти".
         */

        [Test]
        public void SearchWithoutDestination()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModelWithWrongDestinationProp();
                MainPage mainPage = new MainPage(_webDriver, LoggerConfiguration.Log);
                var searchPage =
                    mainPage
                        .OpenPage()
                        .WriteIntoFromField(requestModel.From)
                        .WriteIntoDestinationField(requestModel.To)
                        .ClickToSearchButton();

                Assert.AreEqual("", searchPage.DestinationField.Text);
            }, nameof(SearchWithEqualDepartureAndDestinationPlace));
            
        }
    }
}
