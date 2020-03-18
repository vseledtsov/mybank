using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public double Balance { get; set; }

        public ICollection<Transfer> Incoming { get; set; }

        public ICollection<Transfer> Outgoing { get; set; }

    }
}
