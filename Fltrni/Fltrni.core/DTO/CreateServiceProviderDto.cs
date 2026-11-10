using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.DTO
{
    public class CreateServiceProviderDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Governorate { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public decimal StartingPrice { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int CategoryId { get; set; }


        public string? NewCategoryName { get; set; }

    }
}
