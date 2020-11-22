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
    public class FeeTypeService : IFeeTypeService
    {
        private readonly IFeeTypeRepository _feeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeeTypeService(IFeeTypeRepository feeTypeRepository, IUnitOfWork unitOfWork)
        {
            _feeTypeRepository = feeTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FeeTypeResponse> DeleteAsync(int id)
        {
            var existingFeeType = await _feeTypeRepository.FindById(id);
            if (existingFeeType == null)
                return new FeeTypeResponse("FeeType not found");
            try
            {
                _feeTypeRepository.Remove(existingFeeType);
                await _unitOfWork.CompleteAsync();
                return new FeeTypeResponse(existingFeeType);
            }
            catch (Exception ex)
            {
                return new FeeTypeResponse($"An error ocurred while deleting FeeType: { ex.Message }");
            }
        }

        public async Task<FeeTypeResponse> GetById(int id)
        {
            var existingFeeType = await _feeTypeRepository.FindById(id);
            if (existingFeeType == null)
                return new FeeTypeResponse("FeeType not found");
            return new FeeTypeResponse(existingFeeType);
        }

        public async Task<IEnumerable<FeeType>> ListAsync()
        {
            return await _feeTypeRepository.ListAsync();
        }

        public async Task<FeeTypeResponse> SaveAsync(FeeType feeType)
        {
            try
            {
                await _feeTypeRepository.AddAsync(feeType);
                await _unitOfWork.CompleteAsync();
                return new FeeTypeResponse(feeType);
            }
            catch(Exception ex)
            {
                return new FeeTypeResponse($"An error ocurred while saving FeeType: {ex.InnerException}");
            }
        }

        public async Task<FeeTypeResponse> UpdateAsync(FeeType feeType, int id)
        {
            var existingFeeType = await _feeTypeRepository.FindById(id);
            if (existingFeeType == null)
                return new FeeTypeResponse("FeeType not found");
            existingFeeType.Name = feeType.Name;
            try
            {
                _feeTypeRepository.Update(existingFeeType);
                await _unitOfWork.CompleteAsync();
                return new FeeTypeResponse(existingFeeType);
            }
            catch(Exception ex)
            {
                return new FeeTypeResponse($"An error ocurred while updating FeeType: {ex.InnerException}");
            }
        }
    }
}
