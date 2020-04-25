using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionModel.Classes
{
    public class NduCurrency
    {
        [JsonProperty("r030")]
        public int r030 { get; set; }
        [JsonProperty("txt")]
        public string CurrencyName { get; set; }
        [JsonProperty("rate")]
        public decimal Currency { get; set; }
        [JsonProperty("cc")]
        public string CurrencyCode { get; set; }
        [JsonProperty("exchangedate")]
        public string CurrencyDate { get; set; }
    }
}
