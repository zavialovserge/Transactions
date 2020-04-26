using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionModel;
using TransactionModel.ModelContext;

namespace TransactionWebProject.Controller
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        TransactionDbContext _dbContext { get;  }
        public TransactionController(TransactionDbContext transactionDb)
        {
            this._dbContext = transactionDb;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.Transactions.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        public static DateTime NextDateTime(Random rng)
        {
            int year = 2020;
            int month = rng.Next(1, 4);
            int day = rng.Next(1, 29);
            return new DateTime(year, month, day);
        }

        


        [HttpPut]
        public IActionResult PutTransaction(Transaction transaction)
        {
            
            if (transaction == null)
            {
                return BadRequest();
            }
            if (TransacrionExist(transaction.id))
            {
                return NotFound(transaction);
            }
            try
            {
                _dbContext.Update(transaction);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }
            var newTransaction = _dbContext.Transactions.Find(transaction.id);
            return Ok(newTransaction);
        }

        private bool TransacrionExist(int id)
        {
            return !_dbContext.Transactions.Any(x => x.id == id);
        }

        [HttpPost]
        public ActionResult Post(Transaction transaction)
        {
            if (transaction == null || transaction._currency == null)
            {
                return BadRequest(transaction);
            }
            //if (TransacrionExist(transaction.id)) return BadRequest("SMTH");

            transaction._currency.CurrencyDate = transaction.TransactionDate;
            //TO DO UAH
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (TransacrionExist(id)) return BadRequest();

            _dbContext.Transactions.Remove(_dbContext.Transactions.Find(id));
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
