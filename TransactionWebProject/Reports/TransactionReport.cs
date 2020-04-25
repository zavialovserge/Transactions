using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransactionModel;
using TransactionModel.ModelContext;

namespace TransactionWebProject.Reports
{
    public class TransactionReport
    {
        public Transaction[] _transactions { get; set; }
       


        public string ShowReportQuater()
        {
            string message = string.Empty;
            if (_transactions == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html>
                                <html>
                                <head>
                                <style>
                                table, th, td {
                                  border: 1px solid black;
                                  border-collapse: collapse;
                                }
                                th, td {
                                  padding: 5px;
                                }
                                th {
                                  text-align: left;
                                }
                                </style>
                                </head>
                                <body> 
                                ");
            sb.AppendLine("<table style=\"width: 100 %\">");
            
            sb.AppendLine(@"<tr>
                                  <th>#</th>
                                  <th>Date</th> 
                                  <th>Amount</th>
                                  <th>Currency</th>
                                  <th>Cource</th> 
                                  <th>Amount in UAH</th>
                                </tr>");
            int i = 1;
            foreach (var transaction in _transactions)
            {                
                sb.AppendLine(@$"<tr>
                                  <th>{i}</th>
                                  <th>{transaction.TransactionDate.ToString("dd-MM-yy")}</th> 
                                  <th>{transaction.Amount}</th>
                                  <th>{transaction._currency.CurrencyName}</th>
                                  <th>{transaction._currency.Amount}</th> 
                                  <th>{transaction._currency.Amount * transaction.Amount}</th>
                                </tr>");
                i++;
            }
            sb.AppendLine("</table>");
            sb.AppendLine(@"</body>
                               </html>");
            return sb.ToString();
        }
       
    }
}
