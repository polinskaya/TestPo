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
        private const string ExpectedErrorText = "Введённые вами параметры поиска некорректны.\r\nПожалуйста, измените параметры поиска и запустите новый поиск.";

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
