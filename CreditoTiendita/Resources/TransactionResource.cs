using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class TransactionResource
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public float Payment { get; set; }
        public bool Payed { get; set; }
        public TransactionTypeResource TransactionType { get; set; }
    }
}
