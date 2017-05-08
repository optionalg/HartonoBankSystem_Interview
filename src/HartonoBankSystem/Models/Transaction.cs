using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HartonoBankSystem.Models
{
    public class Transaction
    {
        public enum TransactionTypeEnum
        {
            Deposit, Withdrawal, Transfer
        }

        public int ID { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int AccountID { get; set; }
        public int TargetAccountID { get; set; }

    }
}
