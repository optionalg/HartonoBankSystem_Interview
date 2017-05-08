using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HartonoBankSystem.ViewModels
{
    public class Withdrawal
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
    }
}
