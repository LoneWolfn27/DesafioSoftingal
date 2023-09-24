using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdressController : ControllerBase
    {
        
        /*private static List<Address> addresses = new List<Address> {
            new Address(),
            new Address {Id = 1, Codpostal = "4444-000"}
        };*/
        private readonly IAddressService _addressService;

        public AdressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        //Vai buscar todas as Moradas
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetAddressResponseDTO>>>> Get()
        {
            return Ok(await _addressService.GetAllAddresses());
        }

        //Apenas vai buscar uma modara pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAddressResponseDTO>>> GetSingle(int id)
        {
            return Ok(await _addressService.GetAddressById(id));
        }

        //Adicionar uma mordada
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAddressResponseDTO>>>> AddAddress(AddAddressRequestDTO newAddress)
        {
            return Ok(await _addressService.AddAddress(newAddress));
        }

        //Atualizar uma morada
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetAddressResponseDTO>>>> UpdateAddress(UpdateAddressDTO updatedAddress)
        {
            var response = await _addressService.UpdateAddress(updatedAddress);
            if(response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        //Apagar uma morada por id
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAddressResponseDTO>>> DeleteAddress(int id)
        {
            var response = await _addressService.DeleteAddress(id);
            if(response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}