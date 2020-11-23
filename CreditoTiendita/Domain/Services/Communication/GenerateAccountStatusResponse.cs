using CreditoTiendita.Domain.Models;
using CreditoTiendita.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class GenerateAccountStatusResponse
    {
        
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalPayment { get; set; }
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();


    }
}
