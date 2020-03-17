using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class CreditModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }          

        /// <summary>
        /// Months
        /// </summary>
        public int OriginalTerm { get; set; }
        public int RemainingTerm { get; set; }
        public double Amount { get; set; }
        public double OriginalAmount { get; set; }
    }
}
