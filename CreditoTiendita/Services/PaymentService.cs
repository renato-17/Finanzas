using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Repositories;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAditionalCostRepository _aditionalCostRepository;
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IAccountRepository accountRepository, ITransactionRepository transactionRepository,
            IAditionalCostRepository aditionalCostRepository, IAccountStatusRepository accountStatusRepository, IUnitOfWork unitOfWork, ITransactionTypeRepository transactionTypeRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _aditionalCostRepository = aditionalCostRepository;
            _accountStatusRepository = accountStatusRepository;
            _unitOfWork = unitOfWork;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<GenerateAccountStatusResponse> GenerateAccountStatus(int statusId, string clientId)
        {
            float totalPayment = 0;

            var account = await _accountRepository.FindByClientId(clientId);
            var accountStatus = await _accountStatusRepository.FindByIdAndAccountId(account.Id,statusId);

            var fee = account.Fee;
            if (fee == null)
                return new GenerateAccountStatusResponse();
           
            var period = account.Period;

            var transactions = await _transactionRepository.ListByAccountIdAsync(account.Id);
            transactions = transactions.Where(t =>
           CompareDates(t.Date, accountStatus.StartDate) > 0 &&
           CompareDates(t.Date, accountStatus.EndDate) < 0);

            transactions.ToList().ForEach( (a) =>
            {
                if (!a.Payed) { 
                    a.Payment = CalculatePayment(a.Amount, fee, a.Date,accountStatus.EndDate, period);
                    totalPayment += a.Payment;
                }
            });

            var newAccountStatus = new GenerateAccountStatusResponse
            {
                Id = accountStatus.Id,
                StartDate = accountStatus.StartDate,
                EndDate = accountStatus.EndDate,
                TotalPayment = totalPayment,
                Transactions = (IList<Transaction>)transactions
            };

            return newAccountStatus;
        }

        public async Task<PaymentResponse> PayDebt(Transaction transaction, string clientId, int transactionTypeId)
        {
            var paymentAmount = transaction.Amount;
            var errorMessage = string.Empty;

            var account = await _accountRepository.FindByClientId(clientId);
            if (account == null)
                return new PaymentResponse("Account not found");

            transaction.Account = account;
            transaction.AccountId = account.Id;

            var transactionType = await _transactionTypeRepository.FindById(transactionTypeId);
            if (transactionType == null)
                return new PaymentResponse("TransactionType not found");

            transaction.TransactionType = transactionType;
            transaction.TransactionTypeId = transactionTypeId;

            if (transactionType.Name != "Pago")
                return new PaymentResponse("Transaction must be a payment");

            var fee = account.Fee;
            if (fee == null)
                return new PaymentResponse("Fee not found, please enter a fee");

            var period = account.Period;


            if (account.AvailableCredit + paymentAmount > account.AvailableCredit + account.UsedCredit)
            {
                account.AvailableCredit += account.UsedCredit;
            }
            else
            {
                account.AvailableCredit += paymentAmount;
            }

            var transactions = await _transactionRepository.ListByAccountIdAsync(account.Id);
            transactions = transactions.Where(
                t => t.Payed == false &&
                CompareDates(t.Date, transaction.Date) < 0 &&
                t.TransactionType.Name != "Pago");

            if(transactions.ToList().Count == 0)
                return new PaymentResponse("There is nothing to pay");



            transactions.ToList().ForEach(async (a) =>
            {
                a.Payment = CalculatePayment(a.Amount, fee, a.Date, transaction.Date, period);
                
                if(a.Payment <= paymentAmount)
                {
                    paymentAmount -= a.Payment;
                    a.Payed = true;
                }
                else
                {
                    a.Amount = a.Payment - paymentAmount;
                    paymentAmount = 0;
                    a.Payment = 0;
                }                    

                try
                {
                    _transactionRepository.Update(a);
                    await _unitOfWork.CompleteAsync();

                }catch(Exception ex)
                {
                    errorMessage = $"An error ocurred while trying to pay your debts : {ex.Message}";
                }
            });

            transaction.Payed = true;
            
           

            try
            {
                _accountRepository.Update(account);
                await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.CompleteAsync();
                return new PaymentResponse(clientId, paymentAmount);
            }
            catch (Exception ex)
            {
               return new PaymentResponse($"An error ocurred while trying to pay your debts: {ex.Message}"); 
            }
            
        }

        private float CalculatePayment(float amount, Fee fee, DateTime transactionDate, DateTime paymentDate, Period period)
        {
            string feeType = fee.FeeType.Name;
            var percentage = fee.Percentage;
            int diffDays = (paymentDate - transactionDate).Days;

            int numDays = GetNumDays(period.Type);

            switch (feeType)
            {
                case "Simple":
                    {
                        return CalculateTS(amount, percentage, diffDays);
                    }
                case "Nominal":
                    {
                        return CalculateTN(amount, percentage, numDays, diffDays);
                    }
                case "Efectiva":
                    {
                        return CalculateTE(amount, percentage, numDays, diffDays);
                    }
            }

            return 0;
        }
        private float CalculateTS(float amount,float percentage,int difDays)
        {
            return amount * (1 + (percentage) / 100 * difDays);
        }
        private float CalculateTN(float amount,float percentage,int numDays, int difDays)
        {
            var total = amount * Math.Pow((1 + ((percentage / 100) / numDays)), difDays);
            return Convert.ToSingle(total);
        }
        private float CalculateTE(float amount, float percentage, int numDays, int difDays)
        {
            var total = amount * Math.Pow((1+(percentage/100)), (difDays / numDays));
            return Convert.ToSingle(total);
        }
       
        private int GetNumDays(string period)
        {
            switch (period)
            {
                case "Semanal":{ return 7;}
                case "Quincenal":{return 15;}
                case "Mensual": { return 30; }
                case "Bimestral": { return 60; }
                case "Semestral": { return 180; }
                case "Anual": { return 360; }
            }
            return 0;
        }

        private int CompareDates(DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.CompareTo(dateTime2);
        }
    }
}
