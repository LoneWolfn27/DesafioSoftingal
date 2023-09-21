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

        public AddressService(IMapper mapper)
        {
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
            serviceResponse.Data = addresses.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressResponseDTO>> GetAddressById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();
            var address = addresses.FirstOrDefault(a => a.Id == id);
            serviceResponse.Data = _mapper.Map<GetAddressResponseDTO>(address);
            return serviceResponse;
        }
    }
}