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
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IFeeTypeRepository _feeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IFeeRepository feeRepository, IFeeTypeRepository feeTypeRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            _feeRepository = feeRepository;
            _feeTypeRepository = feeTypeRepository;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task<FeeResponse> DeleteAsync(int id)
        {
            var existingFee = await _feeRepository.FindById(id);
            if (existingFee == null)
                return new FeeResponse("Fee not found");
            try
            {
                _feeRepository.Remove(existingFee);
                await _unitOfWork.CompleteAsync();
                return new FeeResponse(existingFee);
            }
            catch(Exception ex)
            {
                return new FeeResponse($"An error ocurred while removing Fee: { ex.Message}");
            }
        }

        public async Task<FeeResponse> GetById(int id)
        {
            var existingFee = await _feeRepository.FindById(id);
            if (existingFee == null)
                return new FeeResponse("Fee not found");
            return new FeeResponse(existingFee);
        }

        public async Task<IEnumerable<Fee>> ListAsync()
        {
            return await _feeRepository.ListAsync();
        }

        public async Task<FeeResponse> SaveAsync(Fee fee, int feeTypeId, int accountId)
        {
            var existingAccount = await _accountRepository.FindById(accountId);
            if (existingAccount == null)
                return new FeeResponse("Account not found");

            var existingFeeType = await _feeTypeRepository.FindById(feeTypeId);
            if (existingFeeType == null)
                return new FeeResponse("FeeType not found");

            fee.Account = existingAccount;
            fee.AccountId = accountId;
            fee.FeeType = existingFeeType;
            fee.FeeTypeId = feeTypeId;

            try
            {
                await _feeRepository.AddAsync(fee);
                await _unitOfWork.CompleteAsync();
                return new FeeResponse(fee);
            }
            catch(Exception ex)
            {
                return new FeeResponse($"An error ocurred while saving fee: {ex.InnerException}");
            }
        }

        public async Task<FeeResponse> UpdateAsync(Fee fee, int id)
        {
            var existingAccount = await _accountRepository.FindById(id);
            if (existingAccount == null)
                return new FeeResponse("Account not found");

            var existingFee = existingAccount.Fee;
            if (existingFee == null)
                return new FeeResponse("Fee not found");
            
            //Se le deberia agregar un feeTypeid para validad si existe y luego cambiar el objeto y el id
            existingFee.Percentage = fee.Percentage;

            var existingFeeType = await _feeTypeRepository.FindById(fee.FeeTypeId);
            if (existingFeeType == null)
                return new FeeResponse("FeeType not found");

            existingFee.FeeType = fee.FeeType;
            existingFee.FeeTypeId = fee.FeeTypeId;

            try
            {
                _feeRepository.Update(existingFee);
                await _unitOfWork.CompleteAsync();
                return new FeeResponse(existingFee);
            }
            catch (Exception ex)
            {
                return new FeeResponse($"An error ocurred while update fee: {ex.InnerException}");
            }
        }
    }
}
