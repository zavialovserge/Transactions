using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TransactionModel.Classes
{
    public static class NbuApi
    {
        public static decimal GetAmount(string currencyName, DateTime currencyDate)
        {
            if (string.IsNullOrEmpty(currencyName) || currencyDate == DateTime.MinValue || currencyDate == DateTime.MaxValue) return 0.00M;
            string dateInCurFromat = currencyDate.ToString("yyyyMMdd");
            string requestStr = $"http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={currencyName}&date={dateInCurFromat}&json";
            WebRequest request = WebRequest.Create(requestStr);
           
            // Get the response.
            WebResponse response = request.GetResponse();
            List<NduCurrency> nduCurrency = null;

            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    string responseFromServer = reader.ReadToEnd();
        
                    Console.WriteLine(responseFromServer);

                    if (!string.IsNullOrEmpty(responseFromServer))
                    {
                         nduCurrency = JsonConvert.DeserializeObject<List<NduCurrency>>(responseFromServer);
                    }
                    else
                    {
                        return 0.00M;
                    }
                }
            }
            response.Close();
            return nduCurrency.Any()? nduCurrency.Select(a => a.Currency).First()
                                     : 0.00M;
        }
       
    }
}
