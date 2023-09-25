using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            _mapper = mapper;
            _address = address; 
        }

        /*private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);*/

       public Task<ServiceResponse<List<GetAddressResponseDTO>>> AddAddress(AddAddressRequestDTO newAddress)
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();
            var address = _mapper.Map<Address>(newAddress);
            address.Id = addresses.Max(a => a.Id) + 1;
            addresses.Add(address);
            serviceResponse.Data = addresses.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            return Task.FromResult(serviceResponse);
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

        public async Task<ServiceResponse<GetAddressResponseDTO>> UpdateAddress(int id, UpdateAddressDTO updateAddress)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();
            try {
           
                var address = addresses.FirstOrDefault(a => a.Id == updateAddress.Id);
                if(address is null)
                    throw new Exception($"Address with Id '{updateAddress.Id}' not found!!!");
                    _mapper.Map(updateAddress, address);
                address.Morada = updateAddress.Morada;
                address.Codpostal = updateAddress.Codpostal;
                address.Rua = updateAddress.Rua;
                address.Freguesia = updateAddress.Freguesia;
                address.Concelho = updateAddress.Concelho;
                address.Distrito = updateAddress.Distrito;
                address.Pais = updateAddress.Pais;
                serviceResponse.Data = _mapper.Map<GetAddressResponseDTO>(address);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> DeleteAddress(int id)
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