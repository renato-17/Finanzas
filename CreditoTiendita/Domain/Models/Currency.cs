using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }

        public IList<Account> Accounts { get; set; } = new List<Account>();
    }

}
