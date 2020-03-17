using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class TransferModel
    {
        public int RecepientId { get; set; }

        public double Amount { get; set; }
    }
}
