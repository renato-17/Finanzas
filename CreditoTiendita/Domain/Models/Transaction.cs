using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public float Payment { get; set; }

        //Relation with TransactionType
        public int? TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        //Relation with Account
        public int? AccountId { get; set; }
        public Account Account { get; set; }
    }
}
