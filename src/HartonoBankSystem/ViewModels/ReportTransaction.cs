using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HartonoBankSystem.Models;

namespace HartonoBankSystem.ViewModels
{
    public class ReportTransactionSearch
    {
        public Transaction.TransactionTypeEnum TransactionType { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class ReportTransactionResult
    {
        public Transaction.TransactionTypeEnum TransactionType { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
