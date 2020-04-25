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
            string currName = currencyName.ToUpper();
            if (currencyName == "UAH")
            {
                return new BadRequestResult();
            }
            var item = _dbContext.Currencies.Find(currName, currencyDate);
            if (item == null)
            {
                Currency currency = new Currency(currName, currencyDate);
                if (currency != null && currency.Amount != 0.00m)
                {
                    _dbContext.Currencies.Add(currency);
                    _dbContext.SaveChanges();
                    return new ObjectResult(currency);
                }
                return NotFound(currency);
            }
            return new ObjectResult(item);
        }
        

       
    }
}
