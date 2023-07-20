namespace Contoso.Stock.Infrastructure;

public class Constants
{
    public struct Notification 
    {
        public const string BROADCAST = "broadcastMessage";

        public const string GET_STOCK_VALUE = "getStockValue";

        public const string LOAD_INIT_STOCK_MARKET = "loadInitStockMarket";

        public const string RESET_MARKET = "resetMarket";
    }

    public const string HUB_ROUTE = "/stock";
}
