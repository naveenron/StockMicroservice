using StockMicroservice.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StockMicroservice.Services
{
    public class StockServices
    {
        private readonly IMongoCollection<Stocks> stocks;

        public StockServices(IStockMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            stocks = database.GetCollection<Stocks>(settings.StocksCollectionName);
        }

        public List<Stocks> GetAll() =>
            stocks.Find(book => true).ToList();

        public List<Stocks> Get(string CompanyId, DateTime startDate, DateTime EndDate) =>
            stocks.Find<Stocks>(s => s.CompanyID == CompanyId && s.CreatedDate >= startDate && s.CreatedDate <= EndDate).SortByDescending(x => x.CreatedDate).ToList();

        public List<Stocks> GetStockByCompanyId(string CompanyId) =>
            stocks.Find<Stocks>(s => s.CompanyID == CompanyId).SortByDescending(x => x.CreatedDate).ToList();

        public Stocks GetStockPrice(string CompanyId) =>
            stocks.Find<Stocks>(s => s.CompanyID == CompanyId).SortByDescending(x => x.CreatedDate).FirstOrDefault();

        public Stocks Create(string code, decimal stockPrice)
        {
            var createStocks = new Stocks
            {
                CompanyID = code,
                StockPrice = stockPrice,
                CreatedDate = DateTime.Now
            };

            stocks.InsertOne(createStocks);
            return createStocks;
        }

        public void Update(string id, Stocks bookIn) =>
            stocks.ReplaceOne(s => s.StockId == id, bookIn);

        public void Remove(string companyId) =>
            stocks.DeleteMany(s => s.CompanyID == companyId);
    }
}