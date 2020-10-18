using System;
using Newtonsoft.Json.Linq;

namespace stockTrader
{
    /// <summary>
    /// Stock price service that gets prices from a remote API
    /// </summary>
    public class StockAPIService {

        private static readonly string apiPath = "https://run.mocky.io/v3/9e14e086-84c2-4f98-9e36-54928830c980?stock={0}";

        /// <summary>
        /// Get stock price from the API
        /// </summary>
        /// <param name="symbol">Stock symbol, for example "aapl"</param>
        /// <returns>the stock price</returns>
        public double GetPrice(string symbol) {
            string url = String.Format(apiPath, symbol);
            string result = RemoteURLReader.ReadFromUrl(url);
            var json = JObject.Parse(result);
            string price = json.GetValue("price").ToString();
            return double.Parse(price);
    }
	
    /// <summary>
    /// Buys a share of the given stock at the current price. Returns false if the purchase fails 
    /// </summary>
    public bool Buy(string symbol) {
        // Stub. No need to implement this.
        return true;
    }
}
}