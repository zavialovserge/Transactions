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
        TransactionDbContext dbContext { get;  }
        public TransactionController(TransactionDbContext transactionDb)
        {
            this.dbContext = transactionDb;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = dbContext.Transactions.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
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
                dbContext.Update(transaction);
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }
            var newTransaction = dbContext.Transactions.Find(transaction.id);
            return Ok(newTransaction);
        }

        private bool TransacrionExist(int id)
        {
            return !dbContext.Transactions.Any(x => x.id == id);
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

            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
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

            dbContext.Transactions.Remove(dbContext.Transactions.Find(id));
            dbContext.SaveChanges();

            return Ok();
        }

    }
}
