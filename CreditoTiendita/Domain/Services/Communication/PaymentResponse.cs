using CreditoTiendita.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class PaymentResponse
    {
        public string ClientId { get; set; }
        public float TotalPayment { get; set; }
        public bool Succesfull { get; set; }
        public string Message { get; set; }

        public PaymentResponse(string clientDni, float totalPayment)
        {
            ClientId = clientDni;
            TotalPayment = totalPayment;
            Succesfull = true;
            Message = string.Empty;
        }

        public PaymentResponse(string message)
        {
            Message = message;
            Succesfull = false;
        }
    }
}
