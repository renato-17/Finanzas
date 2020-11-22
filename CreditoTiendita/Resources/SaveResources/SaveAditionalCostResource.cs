using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveAditionalCostResource
    {
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
