using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class OverdueCreditModel
    {
        public int Id { get; set; }      
        public double Amount { get; set; }
        public double OriginalAmount { get; set; }
        public string CustomerName { get; set; }
    }
}
