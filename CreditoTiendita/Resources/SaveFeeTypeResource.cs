using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class SaveFeeTypeResource
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
