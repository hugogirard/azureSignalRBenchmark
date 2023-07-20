using Contoso.Stock.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using StockMarket.Api.Service;
using System;

namespace StockMarket.Api.Hub
{
    public class StockTickerHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public const string HubUrl = "/stock";
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        private const string MARKET_KEY = "MARKET_STATE";

        public struct MarketState 
        {
            public const string OPEN = "Open";
            public const string CLOSED = "Closed";
        }

        public StockTickerHub(ICacheService cacheService,ILogger logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task GetStockValue(string symbol)        
        {
            var stock = await _cacheService.GetValue<Stock>(symbol);

            if (stock != null) 
            { 
                await Clients.Client(Context.ConnectionId).SendAsync(Constants.Notification.GET_STOCK_VALUE, stock);
            }            
        }

        public async Task LoadDefaultMarketValue()
        {
            await InitializeStockValue(Constants.Notification.LOAD_INIT_STOCK_MARKET);
        }

        public async Task UpdateStockPrices(string symbol) 
        {
            //var stock = await _cacheService.GetValue<Stock>(symbol);

            //if (stock != null) 
            //{
            //    var percentChange = random.NextDouble() * _rangePercent;
            //    var pos = random.NextDouble() > 0.51;
            //    var change = Math.Round(stock.Price * (decimal)percentChange, 2);
            //    change = pos ? change : -change;

            //    stock.Price += change;
            //}
        }

        public async Task OpenMarket()
        {
            string state = await _cacheService.GetValue<string>(MARKET_KEY);
            if (string.IsNullOrEmpty(state) || state != MarketState.OPEN) 
            { 
                state = MarketState.OPEN;
                await BroadcastMarketStateChange(state);
                await _cacheService.SetValue(MARKET_KEY, state);
            }            
        }

        public async Task CloseMarket()
        {
            string state = await _cacheService.GetValue<string>(MARKET_KEY);
            if (!string.IsNullOrEmpty(state) || state != MarketState.CLOSED)
            {
                state = MarketState.CLOSED;
                await BroadcastMarketStateChange(state);
                await _cacheService.SetValue(MARKET_KEY, state);
            }
        }

        public async Task Reset()
        {            
            string state = await _cacheService.GetValue<string>(MARKET_KEY);
            if (!string.IsNullOrEmpty(state) || state != MarketState.CLOSED)
            {
                _logger.LogInformation("Market need to be closed before reset");
                await Clients.All.SendAsync(Constants.Notification.BROADCAST, "Market need to be closed before reset");
                return;
            }

            await InitializeStockValue(Constants.Notification.RESET_MARKET);

        }

        private async Task BroadcastMarketStateChange(string marketState) 
        {
            await Clients.All.SendAsync(Constants.Notification.BROADCAST,marketState);
        }

        private async Task InitializeStockValue(string method)
        {
            var stocks = new List<Stock>
            {
                new Stock { Symbol = "MSFT", Price = 41.68m },
                new Stock { Symbol = "AAPL", Price = 92.08m },
                new Stock { Symbol = "GOOG", Price = 543.01m }
            };

            foreach (var stock in stocks)
            {
                await _cacheService.SetValue(stock.Symbol, stock);
                await Clients.All.SendAsync(method, stock);
            }
        }
    }
}
