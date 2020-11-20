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
    public class PeriodService : IPeriodService
    {
        private readonly IPeriodRepository _periodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PeriodService(IPeriodRepository periodRepository, IUnitOfWork unitOfWork)
        {
            _periodRepository = periodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PeriodResponse> DeleteAsync(int id)
        {
            var existingPeriod = await _periodRepository.FindById(id);
            if (existingPeriod == null)
                return new PeriodResponse("Period not found");
            try
            {
                _periodRepository.Remove(existingPeriod);
                await _unitOfWork.CompleteAsync();
                return new PeriodResponse(existingPeriod);
            }
            catch (Exception ex)
            {
                return new PeriodResponse($"An error ocurred while deleting Period: { ex.Message }");
            }
        }

        public async Task<PeriodResponse> GetById(int id)
        {
            var existingPeriod = await _periodRepository.FindById(id);
            if (existingPeriod == null)
                return new PeriodResponse("Period not found");
            return new PeriodResponse(existingPeriod);
        }

        public async Task<IEnumerable<Period>> ListAsync()
        {
            return await _periodRepository.ListAsync();
        }

        public async Task<PeriodResponse> SaveAsync(Period period)
        {
            try
            {
                await _periodRepository.AddAsync(period);
                await _unitOfWork.CompleteAsync();
                return new PeriodResponse(period);
            }
            catch (Exception ex)
            {
                return new PeriodResponse($"An error ocurred while saving Period: {ex.InnerException}");
            }
        }

        public async Task<PeriodResponse> UpdateAsync(Period period, int id)
        {
            var existingPeriod = await _periodRepository.FindById(id);
            if (existingPeriod == null)
                return new PeriodResponse("Period not found");
            existingPeriod.Type = period.Type;
            try
            {
                _periodRepository.Update(existingPeriod);
                await _unitOfWork.CompleteAsync();
                return new PeriodResponse(existingPeriod);
            }
            catch (Exception ex)
            {
                return new PeriodResponse($"An error ocurred while updating Period: {ex.InnerException}");
            }
        }
    }
}
