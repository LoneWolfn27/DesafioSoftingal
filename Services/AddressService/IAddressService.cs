using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Services.AddressService
{
    public interface IAddressService
    {
        Task<ServiceResponse<List<Address>>> GetAllAddresses();

        Task<ServiceResponse<Address>> GetAddressById(int id);

        Task<ServiceResponse<List<Address>>> AddAddress(Address newAddress);
    }
}