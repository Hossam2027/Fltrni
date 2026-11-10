using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.DTO
{
    public  class ServiceProviderDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal StartingPrice { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? GoogleMapsUrl { get; set; }
        public string email { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
