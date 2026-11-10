using Fltrni.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int ServiceProviderId { get; set; }
        public ServiceProvider? ServiceProvider { get; set; }

        public string CustomerId { get; set; } = string.Empty;
        public AppUser? Customer { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
