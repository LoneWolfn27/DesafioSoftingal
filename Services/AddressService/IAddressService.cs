using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Services.AddressService
{
    public interface IAddressService
    {
        List<Address> GetAllAddresses();

        Address GetAddressById(int id);

        List<Address> AddAddress(Address newAddress);
    }
}