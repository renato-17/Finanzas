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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResponse> DeleteAsync(int id)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("client not found");

            try
            {
                _clientRepository.Remove(existingClient);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while removing client: { ex.Message}");
            }
        }

        public async Task<ClientResponse> GetById(int id)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("client not found");
            return new ClientResponse(existingClient);
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await _clientRepository.ListAsync();
        }

        public async Task<ClientResponse> SaveAsync(Client client)
        {
            try
            {
                await _clientRepository.AddAsync(client);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(client);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while saving client: {ex.InnerException}");
            }
        }

        public async Task<ClientResponse> UpdateAsync(Client client, int id)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("client not found");

            existingClient.Dni = client.Dni;
            existingClient.Name = client.Name;
            existingClient.LastName = client.LastName;
            existingClient.Birthdate = client.Birthdate;
            existingClient.PhoneNumber = client.PhoneNumber;
            existingClient.Address = client.Address;
            existingClient.Mail = client.Password;

            try
            {
                _clientRepository.Update(existingClient);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while update client: {ex.InnerException}");
            }
        }
    }
}
