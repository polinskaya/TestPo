using SeleniumWebdriver.Models;

namespace SeleniumWebdriver.Services
{
    public static class SearchRequestCreator
    {
        private static readonly string FromKey = "from";
        private static readonly string DestinationKey = "destination";
        private static readonly string WrongDestinationKey = "wrongDestination";


        public static SearchRequestModel CreateSearchRequestModelWithEqualFromAndDestinationProps()
        {
            return new SearchRequestModel
            {
                From = TestDataReader.GetTestData(FromKey),
                To = TestDataReader.GetTestData(DestinationKey)
            };
        }

        public static SearchRequestModel CreateSearchRequestModelWithWrongDestinationProp()
        {
            return new SearchRequestModel
            {
                From = TestDataReader.GetTestData(FromKey),
                To = TestDataReader.GetTestData(WrongDestinationKey)
            };
        }
    }
}
