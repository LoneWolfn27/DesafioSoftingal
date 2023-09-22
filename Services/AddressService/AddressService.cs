using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private static List<Address> addresses = new List<Address> {
            new Address(),
            new Address {Id = 1, Codpostal = "4444-000"}
        };
        private readonly IMapper _mapper;
        private readonly DbAddress _address;
        public AddressService(IMapper mapper, DbAddress address)
        {
            _address = address;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> AddAddress(AddAddressRequestDTO newAddress)
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();
            var address = _mapper.Map<Address>(newAddress);
            address.Id = addresses.Max(a => a.Id) + 1;
            addresses.Add(address);
            serviceResponse.Data = addresses.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> GetAllAddresses()
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();
            var dbAddressess = await _address.Addresses.ToListAsync();
            serviceResponse.Data = dbAddressess.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressResponseDTO>> GetAddressById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();
            var dbAddress = await _address.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            serviceResponse.Data = _mapper.Map<GetAddressResponseDTO>(dbAddress);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressResponseDTO>> UpdateAddress(UpdateAddressDTO updatedAddress)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();

            try {
           
                var address = addresses.FirstOrDefault(a => a.Id == updatedAddress.Id);
                if(address is null)
                    throw new Exception($"Address with Id '{updatedAddress.Id}' not found!!!");

                    _mapper.Map(updatedAddress, address);

                address.Morada = updatedAddress.Morada;
                address.Codpostal = updatedAddress.Codpostal;
                address.Rua = updatedAddress.Rua;
                address.Freguesia = updatedAddress.Freguesia;
                address.Concelho = updatedAddress.Concelho;
                address.Distrito = updatedAddress.Distrito;
                address.Pais = updatedAddress.Pais;

                serviceResponse.Data = _mapper.Map<GetAddressResponseDTO>(address);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> DeleteAddresses(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();

            try {
           
                var address = addresses.FirstOrDefault(a => a.Id == id);
                if(address is null)
                    throw new Exception($"Address with Id '{id}' not found!!!");

                addresses.Remove(address);

                serviceResponse.Data = addresses.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}