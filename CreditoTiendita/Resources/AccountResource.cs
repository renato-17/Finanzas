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
        public FeeResource FeeResource { get; set; }
        public CurrencyResource CurrencyResource { get; set; }
        public PeriodResource PeriodResource { get; set; }
    }
}
