using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public char Symbol { get; set; }
        public string Code { get; set; }
        
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }

}
