using System;
using NUnit.Framework;
using SeleniumWebdriver.Models;
using SeleniumWebdriver.Pages;
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
        public void SearchWithEqualDepartureAndDestinationPlaceTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                MainPage mainPage = new MainPage(_webDriver, LoggerConfiguration.Log);
                var searchResultPage = 
                    mainPage
                        .OpenPage()
                        .WriteIntoFromField(requestModel.From)
                        .WriteIntoDestinationField(requestModel.From)
                        .ClickToSearchButton();

                Assert.AreEqual(ExpectedErrorText, searchResultPage.ErrorMessage.Text);
            }, nameof(SearchWithEqualDepartureAndDestinationPlaceTest));
        }


        /*
           2. Поиск возможного маршрута без ввода Города прибытия
           Шаги:
           Зайти на главную страницу;
           заполнить формы: Город вылета (Минск),Город прибытия (3212312312),
           нажать кнопку "Найти".
         */

        [Test]
        public void SearchWithoutDestinationTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                MainPage mainPage = new MainPage(_webDriver, LoggerConfiguration.Log);
                var searchPage =
                    mainPage
                        .OpenPage()
                        .WriteIntoFromField(requestModel.From)
                        .WriteIntoDestinationField("123")
                        .ClickToSearchButton();

                Assert.AreEqual("", searchPage.DestinationField.Text);
            }, nameof(SearchWithEqualDepartureAndDestinationPlaceTest));
            
        }

        /*
         4. Поиск маршрута для компании от 9 человек
            Шаги:
            Зайти на главную страницу;
            заполнить формы: Город вылета (Минск),
 		             Город прибытия (Варшава),
		             Туда(указать завтрошнюю дату),
 		             Обратно(указать дату через 1 неделю),
		             Кол-во пассажиров (9 взрослых,эконом);
         */

        [Test]
        public void MaxPassengersCountTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var passengersCountTest = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField(requestModel.From)
                    .WriteIntoDestinationField(requestModel.To)
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenReturnCalendar().SelectReturnDate(7)
                    .OpenPassengersControl().AddElderPassengers(10).ApplyPassengers().GetPassengersCount();
                Assert.AreNotEqual("10 пассажиров", passengersCountTest);
            }, nameof(MaxPassengersCountTest));
        }


        /*
         1. Поиск возможного маршрута без ввода даты возвращения 
         Шаги:
            Зайти на главную страницу;
            заполнить формы: 
                Город вылета (Минск),
		        Город прибытия (Будапешт),
		        Туда(указать завтрошнюю дату),
		        Кол-во пассажиров (3 взрослых,эконом);
            нажать кнопку "Найти".
         */

        [Test]
        public void SearchTicketsWithOutReturnDateTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField(requestModel.From)
                    .WriteIntoDestinationField(requestModel.To)
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenPassengersControl().AddElderPassengers(2).ApplyPassengers()
                    .ClickToSearchButton();
                Assert.IsTrue(requestModel.From.Contains(searchResultPage.FromPlace.Text));
                Assert.IsTrue(requestModel.To.Contains(searchResultPage.DestinationPlace.Text));
            }, nameof(SearchTicketsWithOutReturnDateTest));
        }

        /*
         3. Поиск маршрута с прошедшей датой
         Шаги:
            Зайти на главную страницу;
            заполнить формы: Город вылета (Минск),
            Город прибытия (Бостон),
            Туда(указать сегодняшний день -1),
            Обратно(указать дату через 1 неделю),
            Кол-во пассажиров (2 взрослых,эконом);
            нажать кнопку "Найти".
         */
        [Test]
        public void SearchTicketsOnPassedDayTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();

                Assert.Throws<InvalidOperationException>(() =>
                {
                    var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                        .OpenPage()
                        .WriteIntoFromField(requestModel.From)
                        .WriteIntoDestinationField(requestModel.To)
                        .OpenDepartCalendar().SelectDepartDate(-1)
                        .OpenReturnCalendar().SelectReturnDate(7)
                        .OpenPassengersControl().AddElderPassengers(1).ApplyPassengers()
                        .ClickToSearchButton();
                });
            }, nameof(SearchTicketsOnPassedDayTest));
        }


        /*
         9. Бронирование билетов при введении некорректных данных 
            Шаги:
            Зайти на главную страницу;
            заполнить формы: Город вылета (1111),
		             Город прибытия (1234),
		             Туда(указать завтрошнюю дату),
		             Обратно(указать дату на 5 дней позже),
		             Кол-во пассажиров (2 взрослых,эконом);
            нажать кнопку "Найти".
            Ожидаемый результат:пользователь не может сделать этого.
            Полученный результат:открывается новая вкладка родного сайта с автосгенерированным или последним маршрутом. 
         */
        [Test]
        public void SearchTicketsWithWrongFromAndToPlacesTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField("123")
                    .WriteIntoDestinationField("789")
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenReturnCalendar().SelectReturnDate(5)
                    .OpenPassengersControl().AddElderPassengers(1).ApplyPassengers()
                    .ClickToSearchButton();

                Assert.IsTrue(searchResultPage.DestinationPlace.Text.Equals(String.Empty));
            }, nameof(SearchTicketsWithWrongFromAndToPlacesTest));
        }

        /*
            5. Сортировка полученных результатов
            Шаги:
            Зайти на главную страницу;
            заполнить формы: Город вылета (Минск),
		             Город прибытия (Лос-Анджелес),
		             Туда(указать завтрошнюю дату),
		             Обратно(дату через 1 неделю),
		             Кол-во пассажиров (1 взрослый,бизнес-класс);
            нажать кнопку "Найти";
            воспользоваться сортировкой "по времени пути".
            Ожидаемый результат:появление сортировки по времени пути от самых коротких до самых длительных.
         */
        [Test]
        public void SortResultsByTimeTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField(requestModel.From)
                    .WriteIntoDestinationField(requestModel.To)
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenReturnCalendar().SelectReturnDate(7)
                    .OpenPassengersControl().SelectBusinessClassTicketType().ApplyPassengers()
                    .ClickToSearchButton().OpenSortOptionList().SelectSortByTimeOption();

                var firstTicketTime = searchResultPage.GetFirstTicketTime();
                var secondTicketTime = searchResultPage.GetSecondTicketTime();

                Assert.IsTrue(firstTicketTime > secondTicketTime || firstTicketTime == secondTicketTime);
            }, nameof(SortResultsByTimeTest));
        }

        /*
         8. Тестирование чек-бокса
            Шаги:
            Зайти на главную страницу;
            заполнить формы: Город вылета (Минск),
		             Город прибытия (Сочи),
		             Туда(указать завтрошнюю дату),
		             Обратно(указать дату на 2 недели позже),
		             Кол-во пассажиров (1 взрослый,1 ребенок до 2 лет,эконом);
            нажать кнопку "Найти";
            в пункте "Багаж" выбрать чекбокс "Без багажа".
            Ожидаемый результат:появление вариантов для дат,выбранных пользователем,с багажом и ручной кладью.
         */

        [Test]
        public void GetTicketsForAgencyTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField(requestModel.From)
                    .WriteIntoDestinationField(requestModel.To)
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenReturnCalendar().CancelDepartDate()
                    .OpenPassengersControl().SelectBusinessClassTicketType().ApplyPassengers()
                    .ClickToSearchButton().OpenAgencyOptionList().SelectAllAgencyOption().SelectAgencyOption(requestModel.Agency);


                Assert.IsTrue(searchResultPage.AgencyOnTicket.Text.Equals(requestModel.Agency));
            }, nameof(GetTicketsForAgencyTest));
        }

        [Test]
        public void GetTicketsForAviacompanyTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var aviacompanyTitle = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage().ClickToAviacompanyButton().AviaCompanyTitle.Text;


                Assert.IsTrue(aviacompanyTitle.Equals("Купить авиабилеты Победа"));
            }, nameof(GetTicketsForAviacompanyTest));
        }

        [Test]
        public void GetTicketsWithOutDepartDateTest()
        {
            TestWrapper(() =>
            {
                SearchRequestModel requestModel =
                    SearchRequestCreator.CreateSearchRequestModel();
                var searchResultPage = new MainPage(_webDriver, LoggerConfiguration.Log)
                    .OpenPage()
                    .WriteIntoFromField(requestModel.From)
                    .WriteIntoDestinationField(requestModel.To)
                    .OpenDepartCalendar().SelectDepartDate(1)
                    .OpenReturnCalendar().CancelDepartDate()
                    .OpenPassengersControl().SelectBusinessClassTicketType().ApplyPassengers()
                    .ClickToSearchButton();

                Assert.IsTrue(searchResultPage.AllTicketsWithoutDepartPart());
            }, nameof(GetTicketsWithOutDepartDateTest));
        }
    }
}
