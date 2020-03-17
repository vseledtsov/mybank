using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class AddCreditModel
    {
        public int CustomerId { get; set; }
        public int CreditTerm { get; set; }
        public double Amount { get; set; }
    }
}
