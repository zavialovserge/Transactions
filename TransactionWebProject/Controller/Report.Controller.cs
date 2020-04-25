using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransactionModel.ModelContext;
using TransactionWebProject.Reports;

namespace TransactionWebProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController:ControllerBase
    {
        TransactionDbContext dbContext { get; }
        public ReportController(TransactionDbContext transactionDb)
        {
            this.dbContext = transactionDb;
        }

        [HttpGet()]
        [Route("QuaterReport")]
        public ContentResult GetQuaterReport(int quarter)
        {
            if (quarter == 0 || quarter == 5) return null;
            int MinMonth = (quarter-1) * 3;
            int MaxMonth = quarter * 3;

            //ToArray??
            var transactions = dbContext.Transactions
                                        .Include(a => a._currency)
                                        .Where(a=>a.TransactionDate.Month <= MaxMonth && a.TransactionDate.Month > MinMonth)
                                        .ToArray();

            TransactionReport rep = new TransactionReport();
            rep._transactions = transactions;
            var sringResponce = rep.ShowReportQuater();
            return new ContentResult
            {
                ContentType = "text/html",
                Content = sringResponce
            };
        }
       
    }
}
