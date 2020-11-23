using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class AccountResource
    {
        public int Id { get; set; }
        public float UsedCredit { get; set; }
        public float AvailableCredit { get; set; }
        public FeeResource Fee { get; set; }
        public CurrencyResource Currency{ get; set; }
        public PeriodResource Period{ get; set; }
    }
}
