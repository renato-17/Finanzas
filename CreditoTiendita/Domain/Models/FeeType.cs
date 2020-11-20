using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class FeeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Relaton with Fee
        public IList<Fee> Fees { get; set; } = new List<Fee>();
    }
}
