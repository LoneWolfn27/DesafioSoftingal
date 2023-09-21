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

        public async Task<ServiceResponse<List<Address>>> AddAddress(Address newAddress)
        {
            var serviceResponse = new ServiceResponse<List<Address>>();
            addresses.Add(newAddress);
            serviceResponse.Data = addresses;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Address>>> GetAllAddresses()
        {
            var serviceResponse = new ServiceResponse<List<Address>>();
            serviceResponse.Data = addresses;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Address>> GetAddressById(int id)
        {
            var serviceResponse = new ServiceResponse<Address>();
            var address = addresses.FirstOrDefault(a => a.Id == id);
            serviceResponse.Data = address;
            return serviceResponse;
        }
    }
}