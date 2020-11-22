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
        private readonly IFeeTypeRepository _feeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IFeeRepository feeRepository, IFeeTypeRepository feeTypeRepository, IUnitOfWork unitOfWork)
        {
            _feeRepository = feeRepository;
            _feeTypeRepository = feeTypeRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<FeeResponse> SaveAsync(Fee fee, int feeTypeId)
        {
            var existingFeeType = await _feeTypeRepository.FindById(feeTypeId);
            if (existingFeeType == null)
                return new FeeResponse("FeeType not found");
            fee.FeeType = existingFeeType;
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
            var existingFee = await _feeRepository.FindById(id);
            if (existingFee == null)
                return new FeeResponse("Fee not found");
            
            //Se le deberia agregar un feeTypeid para validad si existe y luego cambiar el objeto y el id
            existingFee.Percentage = fee.Percentage;

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
