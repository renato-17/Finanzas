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
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(IUnitOfWork unitOfWork, ICurrencyRepository currencyRepository)
        {
            _unitOfWork = unitOfWork;
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyResponse> DeleteAsync(int id)
        {
            var existingcurrency = await _currencyRepository.FindById(id);
            if (existingcurrency == null)
                return new CurrencyResponse("currency not found");
            try
            {
                _currencyRepository.Remove(existingcurrency);
                await _unitOfWork.CompleteAsync();
                return new CurrencyResponse(existingcurrency);
            }
            catch (Exception ex)
            {
                return new CurrencyResponse($"An error ocurred while removing currency: { ex.Message}");
            }
        }

        public async Task<CurrencyResponse> GetById(int id)
        {
            var existingcurrency = await _currencyRepository.FindById(id);
            if (existingcurrency == null)
                return new CurrencyResponse("currency not found");
            return new CurrencyResponse(existingcurrency);
        }

        public async Task<IEnumerable<Currency>> ListAsync()
        {
            return await _currencyRepository.ListAsync();
        }

        public async Task<CurrencyResponse> SaveAsync(Currency currency)
        {
           
            try
            {
                await _currencyRepository.AddAsync(currency);
                await _unitOfWork.CompleteAsync();
                return new CurrencyResponse(currency);
            }
            catch (Exception ex)
            {
                return new CurrencyResponse($"An error ocurred while saving currency: {ex.InnerException}");
            }
        }

        public async Task<CurrencyResponse> UpdateAsync(Currency currency, int id)
        {
            var existingcurrency = await _currencyRepository.FindById(id);
            if (existingcurrency == null)
                return new CurrencyResponse("currency not found");
            
            existingcurrency.Code = currency.Code;
            existingcurrency.Symbol = currency.Symbol;

            try
            {
                _currencyRepository.Update(existingcurrency);
                await _unitOfWork.CompleteAsync();
                return new CurrencyResponse(existingcurrency);
            }
            catch (Exception ex)
            {
                return new CurrencyResponse($"An error ocurred while update currency: {ex.InnerException}");
            }
        }
    }
}
