using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HartonoBankSystem.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Transaction> TransactionList { get; set; }
    }
}
