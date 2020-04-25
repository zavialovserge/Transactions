using System;
using System.Collections.Generic;
using System.Text;
using TransactionModel.Classes;

namespace TransactionModel.ModelContext
{
    public class Currency
    {
        public Currency()
        {
        }

        public Currency(string currencyName,DateTime currencyDate)
        {
            this.CurrencyName = currencyName;
            this.CurrencyDate = currencyDate;
            Amount =NbuApi.GetAmount(currencyName, CurrencyDate);
        }

       

        public string CurrencyName { get; set; }
        public DateTime CurrencyDate { get; set; }
        public decimal Amount { get; set; }
    }
}
