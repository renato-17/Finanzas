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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IClientRepository clientRepository, ICurrencyRepository currencyRepository, IPeriodRepository periodRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
            _currencyRepository = currencyRepository;
            _periodRepository = periodRepository;
        }

        public async Task<AccountResponse> DeleteAsync(int clientId)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            if (existingClient == null)
                return new AccountResponse("Client not found");

            var existingAccount = existingClient.Account;

            try
            {
                _accountRepository.Remove(existingAccount);
                await _unitOfWork.CompleteAsync();
                return new AccountResponse(existingAccount);
            }
            catch (Exception ex)
            {
                return new AccountResponse($"An error ocurred while removing account: { ex.Message}");
            }
        }

        public async Task<AccountResponse> GetByClientId(int clientId)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            if (existingClient == null)
                return new AccountResponse("Client not found");

            return new AccountResponse(existingClient.Account);
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _accountRepository.ListAsync();
        }

        public async Task<AccountResponse> SaveAsync(int clientId, int currencyId, int periodId, Account account)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            if (existingClient == null)
                return new AccountResponse("Client not found");

            var existingCurrency = await _currencyRepository.FindById(currencyId);
            if (existingCurrency == null)
                return new AccountResponse("Currency not found");

            var existingPeriod = await _periodRepository.FindById(clientId);
            if (existingPeriod == null)
                return new AccountResponse("Period not found");

            account.Client = existingClient;
            account.ClientId = clientId;
            account.Currency = existingCurrency;
            account.CurrencyId = currencyId;
            account.Period = existingPeriod;
            account.PeriodId = periodId;

            try
            {
                await _accountRepository.AddAsync(account);
                await _unitOfWork.CompleteAsync();
                return new AccountResponse(account);
            }
            catch (Exception ex)
            {
                return new AccountResponse($"An error ocurred while adding account: { ex.Message}");
            }
        }

        public async Task<AccountResponse> UpdateAsync(int clientId, int currencyId, int periodId, Account account)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            if (existingClient == null)
                return new AccountResponse("Client not found");

            var existingCurrency = await _currencyRepository.FindById(currencyId);
            if (existingCurrency == null)
                return new AccountResponse("Currency not found");

            var existingPeriod = await _periodRepository.FindById(clientId);
            if (existingPeriod == null)
                return new AccountResponse("Period not found");

            if (existingClient.Account == null)
                return new AccountResponse("Client does not have account");
            var existingAccount = existingClient.Account;

            existingAccount.UsedCredit = account.UsedCredit;
            existingAccount.AvailableCredit = account.AvailableCredit;
            existingAccount.Currency = existingCurrency;
            existingAccount.CurrencyId = currencyId;
            existingAccount.Period = existingPeriod;
            existingAccount.PeriodId = periodId;

            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return new AccountResponse(account);
            }
            catch (Exception ex)
            {
                return new AccountResponse($"An error ocurred while update account: { ex.Message}");
            }
        }
    }
}
