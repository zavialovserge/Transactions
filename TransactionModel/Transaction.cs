using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionModel.ModelContext;

namespace TransactionModel
{
    public class Transaction
    {
        public int id { get; set; }
        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public Currency _currency { get; set; }
    }
}
