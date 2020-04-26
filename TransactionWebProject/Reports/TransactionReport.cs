using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransactionModel;
using TransactionModel.ModelContext;

namespace TransactionWebProject.Reports
{
    public class TransactionReport:AbstractReport
    {
        public Transaction[] _transactions { get; set; }

        public override void GetTxtReport()
        {
            StringBuilder sb = new StringBuilder();
            int countElements = _transactions.Length;
            sb.Append(new string('_',60));
            sb.AppendLine();
            sb.Append($"|###");
            sb.Append($"| Date   ");
            sb.Append($"| Amount ");
            sb.Append($"| Currency ");
            sb.Append($"| Cource ");
            sb.Append($"| Amount in UAH |");
                                
            int i = 1;
            decimal total = 0;
            foreach (var transaction in _transactions)
            {
                decimal totalTransaction = Math.Round(transaction._currency.Amount * transaction.Amount, 2);
                sb.AppendLine();
                sb.Append($"|{i}" + new string(' ',2));
                sb.Append($"|{transaction.TransactionDate.ToString("dd-MM-yy")}");
                sb.Append($"| {transaction.Amount}  ");
                sb.Append($"| {transaction._currency.CurrencyName}      ");
                sb.Append($"| {transaction._currency.Amount}" + new string(' ', 7 - transaction._currency.Amount.ToString().Length));
                sb.Append($"| {totalTransaction}"+new string(' ',14-totalTransaction.ToString().Length)+"|");
                                
                i++;
                total += totalTransaction;
            }
            sb.AppendLine();
            sb.Append(new string('_', 60));
            sb.AppendLine();
            sb.Append($"Total" + new string(' ',29)+ $"|{total}" + "UAH");
            sb.AppendLine();
            decimal SingleTax = Math.Round(total * 0.05M, 2);
            sb.Append("Total Single Tax" + new string(' ', 18) + $"|{ SingleTax}" + "UAH");
            sb.AppendLine();
            sb.Append(new string('_', 60));
            using (StreamWriter sw = new StreamWriter(extentionPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(sb.ToString());
            }
        }

        public string ShowReportQuaterHtml()
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
            decimal total = 0;
            foreach (var transaction in _transactions)
            {
                decimal totalTransaction = Math.Round(transaction._currency.Amount * transaction.Amount,2);
                sb.AppendLine(@$"<tr>
                                  <th>{i}</th>
                                  <th>{transaction.TransactionDate.ToString("dd-MM-yy")}</th> 
                                  <th>{transaction.Amount}</th>
                                  <th>{transaction._currency.CurrencyName}</th>
                                  <th>{transaction._currency.Amount}</th> 
                                  <th>{totalTransaction}</th>
                                </tr>");
                i++;
                total += totalTransaction;
            }
            sb.AppendLine(@$"<tr>                                  
                              <th>Total</th> 
                              <th>{total}</th>
                           </tr>");
            decimal SingleTax = Math.Round(total * 0.05M,2);
            sb.AppendLine(@$"<tr>                                  
                              <th>Total Single Tax</th> 
                              <th>{SingleTax}</th>
                           </tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"</body>
                               </html>");
            return sb.ToString();
        }
       
    }
}
