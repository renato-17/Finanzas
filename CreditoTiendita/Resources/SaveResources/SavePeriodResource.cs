using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SavePeriodResource
    {
        [Required]
        [MaxLength(15)]
        public string Type { get; set; }
    }
}
