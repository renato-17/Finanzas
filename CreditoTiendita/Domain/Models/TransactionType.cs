using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relaton with Transaction
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
