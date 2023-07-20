﻿namespace StockMarket.Api.Service
{
    public interface ICacheService
    {
        Task<T> GetValue<T>(string key) where T : class;
        Task SetValue<T>(string key, T value) where T : class;
    }
}