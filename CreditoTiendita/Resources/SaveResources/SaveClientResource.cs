using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Resources.SaveResources
{
    public class SaveClientResource
    {
        [Required]
        [MaxLength(8)]
        public string Dni { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [MaxLength(50)]
        public string Mail { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

    }
}
