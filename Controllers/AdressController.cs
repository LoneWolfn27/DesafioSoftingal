using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ServiceResponse<List<Address>>>> Get()
        {
            return Ok(await _addressService.GetAllAddresses());
        }

        //Apenas vai buscar uma modara pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> GetSingle(int id)
        {
            return Ok(await _addressService.GetAddressById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Address>>>> AddAddress(Address newAddress)
        {
            return Ok(await _addressService.AddAddress(newAddress));
        }
    }
}