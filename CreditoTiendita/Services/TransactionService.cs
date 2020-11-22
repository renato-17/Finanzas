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
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepository, ITransactionTypeRepository transactionTypeRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _transactionTypeRepository = transactionTypeRepository;
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

        public async Task<TransactionResponse> SaveAsync(Transaction transaction, int transactionTypeId)
        {
            var existingTransactionType = await _transactionTypeRepository.FindById(transactionTypeId);
            if (existingTransactionType == null)
                return new TransactionResponse("TransactionType not found");
            transaction.TransactionType = existingTransactionType;
            try
            {
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
