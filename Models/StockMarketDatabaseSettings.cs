using System;

namespace StockMicroservice.Models
{
    public class StockMarketDatabaseSettings : IStockMarketDatabaseSettings
    {
        public StockMarketDatabaseSettings()
        {
        }

        public string StocksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
