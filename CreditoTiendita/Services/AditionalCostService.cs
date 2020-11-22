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
    public class AditionalCostService : IAditionalCostService
    {
        private readonly IAditionalCostRepository _aditionalCostRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AditionalCostService(IAditionalCostRepository aditionalCostRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _aditionalCostRepository = aditionalCostRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AditionalCostResponse> DeleteAsync(int accountId, int id)
        {
            var existingAditionalCost = await _aditionalCostRepository.FindByIdAndAccountId(accountId, id);
            if (existingAditionalCost == null)
                return new AditionalCostResponse("Aditional cost or Account does not exists");

            try
            {
                _aditionalCostRepository.Remove(existingAditionalCost);
                await _unitOfWork.CompleteAsync();
                return new AditionalCostResponse(existingAditionalCost);
            }
            catch (Exception ex)
            {
                return new AditionalCostResponse($"Error while trying to delete aditional cost: {ex.Message}");
            }
        }

        public async Task<AditionalCostResponse> GetByIdAndAccountId(int accountId, int id)
        {
            var existingAditionalCost = await _aditionalCostRepository.FindByIdAndAccountId(accountId, id);
            if (existingAditionalCost == null)
                return new AditionalCostResponse("Aditional cost does not exist");
            return new AditionalCostResponse(existingAditionalCost);
        }

        public async Task<IEnumerable<AditionalCost>> ListByAccountIdAsync(int accountId)
        {
            return await _aditionalCostRepository.ListByAccountIdAsync(accountId);
        }

        public async Task<AditionalCostResponse> SaveAsync(int accountId, AditionalCost aditionalCost)
        {
            var existingAccount = await _accountRepository.FindById(accountId);
            if (existingAccount == null)
                return new AditionalCostResponse("Account does not exist");

            aditionalCost.Account = existingAccount;
            aditionalCost.AccountId = accountId;

            try
            {
                await _aditionalCostRepository.AddAsync(aditionalCost);
                await _unitOfWork.CompleteAsync();
                return new AditionalCostResponse(aditionalCost);
            }catch(Exception ex)
            {
                return new AditionalCostResponse($"Error while trying to save aditional cost: {ex.Message}");
            }
        }

        public async Task<AditionalCostResponse> UpdateAsync(int accountId,int id, AditionalCost aditionalCost)
        {
            var existingAditionalCost = await _aditionalCostRepository.FindByIdAndAccountId(accountId, id);
            if (existingAditionalCost == null)
                return new AditionalCostResponse("Aditional cost or Account does not exists");

            existingAditionalCost.Amount = aditionalCost.Amount;
            existingAditionalCost.Description = aditionalCost.Description;
            

            try
            {
                _aditionalCostRepository.Update(existingAditionalCost);
                await _unitOfWork.CompleteAsync();
                return new AditionalCostResponse(existingAditionalCost);
            }
            catch (Exception ex)
            {
                return new AditionalCostResponse($"Error while trying to update aditional cost: {ex.Message}");
            }
        }
    }
}
