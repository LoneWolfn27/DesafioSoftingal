using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.DTOS.Address
{
    public class GetAddressResponseDTO
    {
        public int Id { get; set; }
        public string? Morada { get; set; } = "1";
        public string? Codpostal { get; set; } = "4425-339";
        public string? Rua { get; set; } = "R. Central da Folgosa 629";
        public string? Freguesia { get; set; } = "Folgosa";
        public string? Concelho { get; set; } = "Maia";
        public string? Distrito { get; set; } = "Porto";
        public string? Pais { get; set; } = "Portugal";
    }
}