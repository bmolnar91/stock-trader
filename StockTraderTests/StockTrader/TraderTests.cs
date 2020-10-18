using System;
using NUnit.Framework;
using NSubstitute;
using stockTrader.StockTrader;

namespace stockTrader
{
    [TestFixture]
    public class TraderTests
    {
        private Trader _trader;
        private StockAPIService _apiService;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            string apiPath = "https://run.mocky.io/v3/9e14e086-84c2-4f98-9e36-54928830c980?stock={0}";
            var reader = new RemoteURLReader();

            _apiService = Substitute.For<StockAPIService>(apiPath, reader);
            _logger = Substitute.For<ILogger>();
            _trader = new Trader(_apiService, _logger);
        }

        [Test] // Bid was lower than price, Buy() should return false.
        public void TestBidLowerThanPrice()
        {
            double lowerBid = 1.00;
            double price = 2.00;
            string someSymbol = "some symbol";

            _apiService.GetPrice(someSymbol).Returns(price);

            Assert.False(_trader.Buy(someSymbol, lowerBid));
        }

        [Test] // bid was equal or higher than price, Buy() should return true.
        public void TestBidHigherThanPrice()
        {
            double higherBid = 2.00;
            double price = 1.00;
            string someSymbol = "some symbol";

            _apiService.GetPrice(someSymbol).Returns(price);

            Assert.True(_trader.Buy(someSymbol, higherBid));
        }
    }
}
