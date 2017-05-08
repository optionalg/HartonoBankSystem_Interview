using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HartonoBankSystem.ViewModels
{
    public class Deposit
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
    }
}
