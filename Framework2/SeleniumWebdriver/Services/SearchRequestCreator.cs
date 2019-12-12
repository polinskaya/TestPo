using SeleniumWebdriver.Models;

namespace SeleniumWebdriver.Services
{
    public static class SearchRequestCreator
    {
        private static readonly string FromKey = "from";
        private static readonly string DestinationKey = "destination";
        private static readonly string WrongDestinationKey = "agency";


        public static SearchRequestModel CreateSearchRequestModel()
        {
            return new SearchRequestModel
            {
                From = TestDataReader.GetTestData(FromKey),
                To = TestDataReader.GetTestData(DestinationKey),
                Agency = TestDataReader.GetTestData(WrongDestinationKey)
            };
        }


    }
}
