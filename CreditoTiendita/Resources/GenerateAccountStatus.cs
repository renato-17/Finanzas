using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class GenerateAccountStatus
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalPayment { get; set; }
        public IList<TransactionResource> Transactions { get; set; } = new List<TransactionResource>();

    }
}
