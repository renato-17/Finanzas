using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Period
    {
        public int Id { get; set; }
        public string Type { get; set; }



        //Relaton with Account
        public IList<Account> Accounts { get; set; } = new List<Account>();

    }
}
