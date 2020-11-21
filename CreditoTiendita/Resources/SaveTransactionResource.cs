using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources
{
    public class SaveTransactionResource
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(15)]
        public string Description { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
    }
}
