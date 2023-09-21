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

        public List<Address> AddAddress(Address newAddress)
        {
            addresses.Add(newAddress);
            return addresses;
        }

        public List<Address> GetAllAddresses()
        {
            return addresses;
        }

        public Address GetAddressById(int id)
        {
            var address = addresses.FirstOrDefault(a => a.Id == id);
            if(address is not null)
                return address;
            throw new Exception("Adress Not FOund!!!");
        }
    }
}