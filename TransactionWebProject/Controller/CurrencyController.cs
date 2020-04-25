using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionModel.ModelContext;

namespace TransactionWebProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController:ControllerBase
    {
        TransactionDbContext _dbContext { get;  }
        public CurrencyController(TransactionDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get(string currencyName,DateTime currencyDate)
        {
            var item = _dbContext.Currencies.Find(currencyName,currencyDate);
            if (item == null)
            {
                Currency currency = new Currency(currencyName, currencyDate);
                if (currency != null && currency.Amount != 0.00m)
                {
                    _dbContext.Currencies.Add(currency);
                    _dbContext.SaveChanges();
                    return new ObjectResult(item);
                }
                return NotFound(currency);
            }
            return new ObjectResult(item);
        }
        
        [HttpPost]
        public ActionResult Post(Currency currency)
        {
            if (currency == null )
            {
                return BadRequest(currency);
            }
            //if (TransacrionExist(transaction.id)) return BadRequest("SMTH");
            if(_dbContext.Currencies.Find(currency.CurrencyName,currency.CurrencyDate) != null) return BadRequest(currency);
            _dbContext.Currencies.Add(currency);
            _dbContext.SaveChanges();
            return Ok(currency);
        }
    }
}
