using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Entities
{
    public class Transfer
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.UtcNow;

        public int SenderId { get; set; }

        public Account Sender { get; set; }

        public int RecepientId { get; set; }

        public Account Recepient { get; set; }

        public double Amount { get; set; }
    }
}
