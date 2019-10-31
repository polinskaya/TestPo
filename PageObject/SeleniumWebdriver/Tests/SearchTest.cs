using NUnit.Framework;
using Pages.Pages;

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
            MainPage mainPage = new MainPage(_webDriver);
            var searchResultPage = mainPage.WriteIntoFromField("Минск,Беларусь")
                .WriteIntoDestinationField("Минск,Беларусь").ClickToSearchButton();

            Assert.AreEqual(ExpectedErrorText, searchResultPage.ErrorMessage.Text);
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
            MainPage mainPage = new MainPage(_webDriver);
            var searchPage = mainPage.WriteIntoFromField("Минск,Беларусь")
                .WriteIntoDestinationField("3212312312").ClickToSearchButton();

            Assert.AreEqual("", searchPage.DestinationField.Text);
        }
    }
}
