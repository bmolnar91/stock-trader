using NUnit.Framework;

namespace stockTrader
{
    public class StockAPIServiceTest
    {
        [Test] // everything works
        public void TestGetPriceNormalValues()
        {
            var toTest = new StockAPIService();
        }

        [Test] // readFromURL threw an exception
        public void TestGetPriceServerDown()
        {
        }

        [Test] // readFromURL returned wrong JSON
        public void TestGetPriceMalformedResponse() 
        {
        }
    }
}