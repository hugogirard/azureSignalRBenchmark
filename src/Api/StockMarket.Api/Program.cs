using Contoso.Stock.Infrastructure;
using StockMarket.Api.Hub;
using StockMarket.Api.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICacheService,CacheService>();

// Add SignalR
builder.Services.AddSignalR().AddAzureSignalR();

var app = builder.Build();

// Configure Hub
app.MapHub<StockTickerHub>(Constants.HUB_ROUTE);

app.UseHttpsRedirection();

app.Run();

