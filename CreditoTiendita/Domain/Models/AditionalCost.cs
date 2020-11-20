using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class AditionalCost
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
