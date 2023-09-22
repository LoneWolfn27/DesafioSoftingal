using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Services.AddressService
{
    public interface IAddressService
    {
        Task<ServiceResponse<List<GetAddressResponseDTO>>> GetAllAddresses();

        Task<ServiceResponse<GetAddressResponseDTO>> GetAddressById(int id);

        Task<ServiceResponse<List<GetAddressResponseDTO>>> AddAddress(AddAddressRequestDTO newAddress);
        Task<ServiceResponse<GetAddressResponseDTO>> UpdateAddress(UpdateAddressDTO newAddress);

    }
}