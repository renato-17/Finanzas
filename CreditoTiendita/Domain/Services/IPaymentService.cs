using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> PayDebt(Transaction transaction, string clientId, int transactionTypeId);
        Task<GenerateAccountStatusResponse> GenerateAccountStatus(int statusId, string clientId);
    }
}
