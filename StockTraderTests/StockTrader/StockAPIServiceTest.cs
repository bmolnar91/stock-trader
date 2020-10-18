using NUnit.Framework;
using NSubstitute;
using System.IO;
using Newtonsoft.Json;

namespace stockTrader
{
    [TestFixture]
    public class StockAPIServiceTest
    {
        private readonly string apiPath = "https://run.mocky.io/v3/9e14e086-84c2-4f98-9e36-54928830c980?stock={0}";
        private readonly string onlyViableURL = "https://run.mocky.io/v3/9e14e086-84c2-4f98-9e36-54928830c980?stock=aapl";
        private readonly string onlyViableResponse = "{\n" +
            "  \"symbol\" : \"AAPL\",\n" +
            "  \"price\" : 338.85\n" +
            "}";

        private StockAPIService _stockService;
        private RemoteURLReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = Substitute.For<RemoteURLReader>();
            _stockService = new StockAPIService(apiPath, _reader);
        }

        [Test] // everything works
        public void TestGetPriceNormalValues()
        {
            string existingSymbol = "aapl";

            _reader.ReadFromUrl(onlyViableURL).Returns(onlyViableResponse);

            double actual = _stockService.GetPrice(existingSymbol);
            double expected = 338.85;

            Assert.AreEqual(expected, actual);
        }

        [Test] // readFromURL threw an exception
        public void TestGetPriceServerDown()
        {
            string existingSymbol = "aapl";

            _reader.When(x => x.ReadFromUrl(onlyViableURL)).Do(x => { throw new IOException(); });

            Assert.Throws(typeof(IOException), delegate { _stockService.GetPrice(existingSymbol); });
        }

        [Test] // readFromURL returned wrong JSON
        public void TestGetPriceMalformedResponse() 
        {
            string existingSymbol = "aapl";
            string malformedResponse = "this is a malformed JSON format";

            _reader.ReadFromUrl(onlyViableURL).Returns(malformedResponse);

            Assert.Throws(typeof(JsonReaderException), delegate { _stockService.GetPrice(existingSymbol); });
        }
    }
}
