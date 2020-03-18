using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Models
{
    public class TransferModel
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.UtcNow;

        public int SenderId { get; set; }      

        public int RecipientId { get; set; }    

        public double Amount { get; set; }

        public string RecipientName { get; set; }

        public string SenderName { get; set; }
    }
}
