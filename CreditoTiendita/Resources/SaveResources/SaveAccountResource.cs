using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveAccountResource
    {
        [Required]
        public float UsedCredit { get; set; }
        [Required]
        public float AvailableCredit { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public int PeriodId { get; set; }
    }
}
