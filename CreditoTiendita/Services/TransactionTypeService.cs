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
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository, IUnitOfWork unitOfWork)
        {
            _transactionTypeRepository = transactionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionTypeResponse> DeleteAsync(int id)
        {
            var existingTransactionType = await _transactionTypeRepository.FindById(id);
            if (existingTransactionType == null)
                return new TransactionTypeResponse("TransactionType not found");
            try
            {
                _transactionTypeRepository.Remove(existingTransactionType);
                await _unitOfWork.CompleteAsync();
                return new TransactionTypeResponse(existingTransactionType);
            }
            catch (Exception ex)
            {
                return new TransactionTypeResponse($"An error ocurred while deleting TransactionType: { ex.Message }");
            }
        }

        public async Task<TransactionTypeResponse> GetById(int id)
        {
            var existingTransactionType = await _transactionTypeRepository.FindById(id);
            if (existingTransactionType == null)
                return new TransactionTypeResponse("TransactionType not found");
            return new TransactionTypeResponse(existingTransactionType);
        }

        public async Task<IEnumerable<TransactionType>> ListAsync()
        {
            return await _transactionTypeRepository.ListAsync();
        }

        public async Task<TransactionTypeResponse> SaveAsync(TransactionType transactionType)
        {
            try
            {
                await _transactionTypeRepository.AddAsync(transactionType);
                await _unitOfWork.CompleteAsync();
                return new TransactionTypeResponse(transactionType);
            }
            catch (Exception ex)
            {
                return new TransactionTypeResponse($"An error ocurred while saving TransactionType: {ex.InnerException}");
            }
        }

        public async Task<TransactionTypeResponse> UpdateAsync(TransactionType transactionType, int id)
        {
            var existingTransactionType = await _transactionTypeRepository.FindById(id);
            if (existingTransactionType == null)
                return new TransactionTypeResponse("TransactionType not found");
            existingTransactionType.Name = transactionType.Name;
            existingTransactionType.Description = transactionType.Description;
            try
            {
                _transactionTypeRepository.Update(existingTransactionType);
                await _unitOfWork.CompleteAsync();
                return new TransactionTypeResponse(existingTransactionType);
            }
            catch (Exception ex)
            {
                return new TransactionTypeResponse($"An error ocurred while updating TransactionType: {ex.InnerException}");
            }
        }
    }
}
