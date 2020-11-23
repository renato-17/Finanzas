using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class FeeResource
    {
        public int Id { get; set; }
        public float Percentage { get; set; }
        public DateTime Date { get; set; } 
        public FeeTypeResource FeeType { get; set; }
    }
}
