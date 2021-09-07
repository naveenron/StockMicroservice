using Microsoft.AspNetCore.Mvc;
using StockMicroservice.Services;
using System;
using System.Globalization;

namespace StockMicroservice.Controllers
{
    [Route("api/v1.0/market/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private StockServices stockServices;

        public StockController(StockServices _stockServices)
        {
            if(_stockServices == null)
            {
                throw new NullReferenceException();
            }

            this.stockServices = _stockServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var stocks = stockServices.GetAll();
                if (stocks == null)
                {
                    return NotFound();
                }

                return Ok(stocks);
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        [HttpGet]
        [Route("get/{comapanyCode}/{startDate}/{endDate}")]
        public IActionResult GetStocksByCompayId(string comapanyCode, DateTime startDate, DateTime endDate)
        {
            try
            {                
                var stocks = stockServices.Get(comapanyCode, startDate, endDate);
                if (stocks == null)
                {
                    return NotFound();
                }
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetStocksByCompayId(string comapanyCode)
        {
            try
            {
                var stocks = stockServices.GetStockByCompanyId(comapanyCode);                
                if (stocks == null)
                {
                    return NotFound();
                }
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("get/{comapanyCode}")]
        public IActionResult GetStocksPrice(string comapanyCode)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var stocks = stockServices.GetStockPrice(comapanyCode);
                if (stocks == null)
                {
                    return Ok(0); ;
                }

                decimal latestStockPrice = stocks.StockPrice;
                return Ok(latestStockPrice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [Route("add/{comapanyCode}")]
        [HttpPost]
        public IActionResult PostStockPrice(string comapanyCode, decimal stockPrice)
        {
            try
            {
                if(stockPrice == 0)
                {
                    return this.BadRequest("Stock Price should not be null or 0");
                }

                var stocks = stockServices.Create(comapanyCode, stockPrice);
                if (stocks == null)
                {
                    return this.BadRequest();
                }

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("delete/{code}")]
        [HttpDelete]
        public IActionResult DeleteStockPriceByCompanyCode(string code)
        {
            try
            {
                stockServices.Remove(code);                
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
