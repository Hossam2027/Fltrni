using Fltrni.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Entities
{
    //[Index(nameof(Email), IsUnique = false)]

    public class ServiceProvider
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal StartingPrice { get; set; }
        public double Rating { get; set; } = 0;
        public int ReviewsCount { get; set; } = 0;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ProviderStatus Status { get; set; } = ProviderStatus.Pending;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }

    }
    public enum ProviderStatus { Pending, Approved, Rejected }

}
