using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveCurrencyResource
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
