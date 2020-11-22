using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveFeeResource
    {
        [Required]
        public float Percentage { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int FeeTypeId { get; set; }
    }
}
