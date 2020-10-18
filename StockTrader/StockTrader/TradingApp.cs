using stockTrader.StockTrader;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

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

			ILogger logger = new FileLogger();
		
	    try {
		    bool purchased = Trader.Instance.Buy(symbol, price);
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