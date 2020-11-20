using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public float Percentage { get; set; }
        public DateTime Date { get; set; }


        //Relation with FeeType
        public int? FeeTypeId { get; set; }
        public FeeType FeeType { get; set; }
        //Relation with Account
        public int? AccountId { get; set; }
        public Account Account { get; set; }
    }
}
