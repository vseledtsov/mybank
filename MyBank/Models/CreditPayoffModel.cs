using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class CreditPayoffModel
    {       
        /// <summary>
        /// Bank Account Id
        /// </summary>
        public int RecipientId { get; set; }

        public double Amount { get; set; }
    }
}
