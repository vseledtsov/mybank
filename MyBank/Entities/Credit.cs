using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Entities
{
    public class Credit
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreditDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Months
        /// </summary>
        public int CreditTerm { get; set; }
        public double Amount { get; set; }
        public double OriginalAmount { get; set; }

    }
}
