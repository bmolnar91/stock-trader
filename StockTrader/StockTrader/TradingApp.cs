using stockTrader.StockTrader;
using System;

namespace stockTrader
{

  internal class TradingApp
  {
    public static void Main(string[] args)
    {
	    TradingApp app = new TradingApp();
	    app.Start();
    }

    public void Start()
    {
	    Console.WriteLine("Enter a stock symbol (for example aapl):");
	    string symbol = Console.ReadLine();
	    Console.WriteLine("Enter the maximum price you are willing to pay: ");
	    double price;
	    while (!double.TryParse(Console.ReadLine(), out price))
	    {
		    Console.WriteLine("Please enter a number.");
	    }

		string apiPath = "https://run.mocky.io/v3/9e14e086-84c2-4f98-9e36-54928830c980?stock={0}";

		RemoteURLReader reader = new RemoteURLReader();
		StockAPIService stockService = new StockAPIService(apiPath, reader);
		ILogger logger = new FileLogger();
		Trader trader = new Trader(stockService, logger);
		
	    try {
		    bool purchased = trader.Buy(symbol, price);
		    if (purchased) {
			    logger.Log("Purchased stock!");
		    }
		    else {
			    logger.Log("Couldn't buy the stock at that price.");
		    }
	    } catch (Exception e) {
		    logger.Log("There was an error while attempting to buy the stock: " + e.Message);
	    }
        Console.ReadLine();
    }
  }
}
