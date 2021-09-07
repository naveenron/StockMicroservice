using StockMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroservice.Infrastructure
{
    public interface ICompanyRepositories
    {
        List<Stocks> GetAllCompany();

        Stocks GetCompanyById(string code);

        Stocks CreateCompany(Stocks companyDetails);
    }
}
