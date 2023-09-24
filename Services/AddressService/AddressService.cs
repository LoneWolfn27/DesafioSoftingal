using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DesafioSoftingal.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly DbAddress _address;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressService(IMapper mapper, DbAddress address, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _address = address; 
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> AddAddress(AddAddressRequestDTO newAddress)
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();
            var address = _mapper.Map<Address>(newAddress);
            address.User = await _address.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _address.Addresses.Add(address);
            await _address.SaveChangesAsync();

            serviceResponse.Data =
                await _address.Addresses
                    .Where(a => a.User!.Id == GetUserId())
                    .Select(a => _mapper.Map<GetAddressResponseDTO>(a))
                    .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAddressResponseDTO>>> GetAllAddresses()
        {
            var serviceResponse = new ServiceResponse<List<GetAddressResponseDTO>>();
            var dbAddress = await _address.Addresses
                .Include(a => a.Codpostal)
                .Include(a => a.Rua)
                .Where(a => a.User!.Id == GetUserId())
                .ToListAsync();
            serviceResponse.Data = dbAddress.Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressResponseDTO>> GetAddressById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();
            var dbAddress = await _address.Addresses
                .Include(a => a.Codpostal)
                .Include(a => a.Rua)
                .FirstOrDefaultAsync(a => a.Id == id && a.User!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetAddressResponseDTO>(dbAddress);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressResponseDTO>> UpdateAddress(UpdateAddressDTO updatedAddress)
        {
            var serviceResponse = new ServiceResponse<GetAddressResponseDTO>();

            try
            {
                var address =
                    await _address.Addresses
                        .Include(a => a.User)
                        .FirstOrDefaultAsync(c => c.Id == updatedAddress.Id);
                if (address is null || address.User!.Id != GetUserId())
                    throw new Exception($"Character with Id '{updatedAddress.Id}' not found.");

                address.Codpostal = updatedAddress.Codpostal;
                address.Rua = updatedAddress.Rua;
                address.Freguesia = updatedAddress.Freguesia;
                address.Concelho = updatedAddress.Concelho;
                address.Distrito = updatedAddress.Distrito;
                address.Pais = updatedAddress.Pais;

                await _address.SaveChangesAsync();
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

            try
            {
                var address = await _address.Addresses
                    .FirstOrDefaultAsync(a => a.Id == id && a.User!.Id == GetUserId());
                if (address is null)
                    throw new Exception($"Address with Id '{id}' not found.");

                _address.Addresses.Remove(address);

                await _address.SaveChangesAsync();

                serviceResponse.Data =
                    await _address.Addresses
                        .Where(a => a.User!.Id == GetUserId())
                        .Select(a => _mapper.Map<GetAddressResponseDTO>(a)).ToListAsync();
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