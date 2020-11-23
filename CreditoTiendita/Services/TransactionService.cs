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
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepository, ITransactionTypeRepository transactionTypeRepository, IAccountRepository accountRepository,IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionResponse> DeleteAsync(int id)
        {
            var existingTransaction = await _transactionRepository.FindById(id);
            if (existingTransaction == null)
                return new TransactionResponse("Transaction not found");
            try
            {
                _transactionRepository.Remove(existingTransaction);
                await _unitOfWork.CompleteAsync();
                return new TransactionResponse(existingTransaction);
            }
            catch (Exception ex)
            {
                return new TransactionResponse($"An error ocurred while removing Transaction: { ex.Message}");
            }
        }

        public async Task<TransactionResponse> GetById(int id)
        {
            var existingTransaction = await _transactionRepository.FindById(id);
            if (existingTransaction == null)
                return new TransactionResponse("Transaction not found");
            return new TransactionResponse(existingTransaction);
        }

        public async Task<IEnumerable<Transaction>> ListAsync()
        {
            return await _transactionRepository.ListAsync();
        }

        public async Task<IEnumerable<Transaction>> ListByAccountIdAsync(int accountId)
        {
            return await _transactionRepository.ListByAccountIdAsync(accountId);
        }

        public async Task<TransactionResponse> SaveAsync(Transaction transaction, int transactionTypeId, int accountId)
        {
            var existingAccount = await _accountRepository.FindById(accountId);
            if (existingAccount == null)
                return new TransactionResponse("Account not found");

            var existingTransactionType = await _transactionTypeRepository.FindById(transactionTypeId);
            if (existingTransactionType == null)
                return new TransactionResponse("TransactionType not found");

            transaction.TransactionType = existingTransactionType;
            transaction.Account = existingAccount;

            if (existingTransactionType.Name == "Pago")
            {
                transaction.Payed = true;
            }
            else
            {
                if (transaction.Amount > existingAccount.AvailableCredit)
                    return new TransactionResponse("You don't have enough credit");


                existingAccount.AvailableCredit -= transaction.Amount;
                existingAccount.UsedCredit = transaction.Amount;
            }

            try
            {
                _accountRepository.Update(existingAccount);
                await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.CompleteAsync();
                return new TransactionResponse(transaction);
            }
            catch (Exception ex)
            {
                return new TransactionResponse($"An error ocurred while saving Transaction: {ex.InnerException}");
            }
        }

        public async Task<TransactionResponse> UpdateAsync(Transaction transaction, int id)
        {
            var existingTransaction = await _transactionRepository.FindById(id);
            if (existingTransaction == null)
                return new TransactionResponse("Transaction not found");

            existingTransaction.Date  = transaction.Date;
            existingTransaction.Description = transaction.Description;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Payment = transaction.Payment;
            existingTransaction.Payed = transaction.Payed;

            try
            {
                _transactionRepository.Update(existingTransaction);
                await _unitOfWork.CompleteAsync();
                return new TransactionResponse(existingTransaction);
            }
            catch (Exception ex)
            {
                return new TransactionResponse($"An error ocurred while update Transaction: {ex.InnerException}");
            }
        }
    }
}
