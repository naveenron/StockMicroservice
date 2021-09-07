using StockMicroservice.Infrastructure;
using StockMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroservice.Feature
{
    public class CompanyRepostories : ICompanyRepositories
    {
        private StockMarketDatabaseSettings stockMarketContext;

        public CompanyRepostories(StockMarketDatabaseSettings stockMarketContext)
        {
            this.stockMarketContext = stockMarketContext;
        }

        public List<Stocks> GetAllCompany()
        {
            if(stockMarketContext != null)
            {                
                var result = (from c in stockMarketContext.CompanyDetails
                             orderby c.CompanyName
                             select c).ToList();

                return result;
            }

            return null;
        }

        public Stocks GetCompanyById(string code)
        {
            if (stockMarketContext != null)
            {
                var result = (from c in stockMarketContext.CompanyDetails
                              where c.CompanyCode == code
                              orderby c.CompanyName
                              select c).FirstOrDefault();

                return result;
            }

            return null;
        }

        public Stocks CreateCompany(Stocks companyDetails)
        {
            if(companyDetails != null)
            {
                stockMarketContext.Add(companyDetails);
                stockMarketContext.SaveChanges();

                var result = (from c in stockMarketContext.CompanyDetails
                              where c.CompanyCode == companyDetails.CompanyCode
                              orderby c.CompanyName
                              select c).FirstOrDefault();

                return result;
            }

            return null;
        }
    }
}
