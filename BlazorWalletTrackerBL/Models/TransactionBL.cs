using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWalletTrackerBL.Models
{
    public class TransactionBL
    {
        public int TransactionId { get; set; }

        public int UserId { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public override string ToString()
        {
            return $"This is TRANSACTIONBL: \n TransactionID: {TransactionId} HE HEHE HEH HE";
        }
    }
}
