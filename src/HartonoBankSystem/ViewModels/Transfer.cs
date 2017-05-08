using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HartonoBankSystem.ViewModels
{
    public class Transfer
    {
        public string AccountNumberFrom { get; set; }
        public string AccountNumberTo { get; set; }

        public int TargetAccountId { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
    }
}
