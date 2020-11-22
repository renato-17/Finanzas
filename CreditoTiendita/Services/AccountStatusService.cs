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
    public class AccountStatusService: IAccountStatusService
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountStatusService(IAccountStatusRepository accountStatusRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountStatusRepository = accountStatusRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountStatusResponse> DeleteAsync(int accountId, int id)
        {
            var existingAccountStatus = await _accountStatusRepository.FindByIdAndAccountId(accountId, id);
            if (existingAccountStatus == null)
                return new AccountStatusResponse("Account status or Account does not exists");

            try
            {
                _accountStatusRepository.Remove(existingAccountStatus);
                await _unitOfWork.CompleteAsync();
                return new AccountStatusResponse(existingAccountStatus);
            }
            catch (Exception ex)
            {
                return new AccountStatusResponse($"Error while trying to delete account status: {ex.Message}");
            }
        }

        public async Task<AccountStatusResponse> GetByIdAndAccountId(int accountId, int id)
        {
            var existingAccountStatus = await _accountStatusRepository.FindByIdAndAccountId(accountId, id);
            if (existingAccountStatus == null)
                return new AccountStatusResponse("Account status does not exist");
            return new AccountStatusResponse(existingAccountStatus);
        }

        public async Task<IEnumerable<AccountStatus>> ListByAccountIdAsync(int accountId)
        {
            return await _accountStatusRepository.ListByAccountIdAsync(accountId);
        }

        public async Task<AccountStatusResponse> SaveAsync(int accountId, AccountStatus accountStatus)
        {
            var existingAccount = await _accountRepository.FindById(accountId);
            if (existingAccount == null)
                return new AccountStatusResponse("Account does not exist");

            accountStatus.Account = existingAccount;
            accountStatus.AccountId = accountId;

            try
            {
                await _accountStatusRepository.AddAsync(accountStatus);
                await _unitOfWork.CompleteAsync();
                return new AccountStatusResponse(accountStatus);
            }
            catch (Exception ex)
            {
                return new AccountStatusResponse($"Error while trying to save account status: {ex.Message}");
            }
        }

        public async Task<AccountStatusResponse> UpdateAsync(int accountId, int id, AccountStatus accountStatus)
        {
            var existingAccountStatus = await _accountStatusRepository.FindByIdAndAccountId(accountId, id);
            if (existingAccountStatus == null)
                return new AccountStatusResponse("Account status or Account does not exists");

            existingAccountStatus.StartDate = accountStatus.StartDate;
            existingAccountStatus.EndDate = accountStatus.EndDate;

            try
            {
                _accountStatusRepository.Update(existingAccountStatus);
                await _unitOfWork.CompleteAsync();
                return new AccountStatusResponse(existingAccountStatus);
            }
            catch (Exception ex)
            {
                return new AccountStatusResponse($"Error while trying to update account status: {ex.Message}");
            }
        }
    }
}
