using System;
using System.Threading;
using NUnit.Framework;

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
            var searchButton = GetWebElement("//BUTTON[@type='submit'][text()='НАЙТИ']");
            var destinationText = GetWebElement("//INPUT[@id='twidget-destination']");
            destinationText.SendKeys("Минск,Беларусь");
            destinationText.SendKeys("\t");
            destinationText.SendKeys("\r\n");
            var errorMessage = GetWebElement("//DIV[@class='description description--bad_search_params']");
            Assert.AreEqual(ExpectedErrorText, errorMessage.Text);
        }


        /*
           2. Поиск возможного маршрута без ввода Города прибытия
           Шаги:
           Зайти на главную страницу;
           заполнить формы: Город вылета (Минск),
           нажать кнопку "Найти".
         */

        [Test]
        public void SearchWithoutDestination()
        {
            var searchButton = GetWebElement("//BUTTON[@type='submit'][text()='НАЙТИ']");
            var destinationText = GetWebElement("//INPUT[@id='twidget-destination']");
            searchButton.Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            var sdfg = destinationText.GetAttribute("title");
            Assert.AreEqual("Заполните это поле.", destinationText.GetAttribute("Title"));
        }
    }
}
