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
        public ActionResult<List<Address>> Get()
        {
            return Ok(_addressService.GetAllAddresses());
        }

        //Apenas vai buscar uma modara pelo ID
        [HttpGet("{id}")]
        public ActionResult<Address> GetSingle(int id)
        {
            return Ok(_addressService.GetAddressById(id));
        }

        [HttpPost]
        public ActionResult<List<Address>> AddAddress(Address newAddress)
        {
            return Ok(_addressService.AddAddress(newAddress));
        }
    }
}