using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveCurrencyResource
    {
        [Required]
        public char Symbol { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
