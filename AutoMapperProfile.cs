using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, GetAddressResponseDTO>();
            CreateMap<AddAddressRequestDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();
        }
    }
}