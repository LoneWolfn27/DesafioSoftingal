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
           var response = await _addressService.GetAddressById(id);
            if (response is null)
                return NotFound("Address not found!!");

            return Ok(response);
        }

        //Adicionar uma mordada
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAddressResponseDTO>>>> AddAddress(AddAddressRequestDTO newAddress)
        {
            var response = await _addressService.AddAddress(newAddress);
            return Ok(response);
        }

        //Atualizar uma morada
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetAddressResponseDTO>>>> UpdateAddress(int id, UpdateAddressDTO updatedAddress)
        {
            var response = await _addressService.UpdateAddress(id, updatedAddress);
            if(response.Data is null)
            {
                return NotFound("Address not found!!");
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
                return NotFound("Address not found!!");
            }

            return Ok(response);
        }
    }
}