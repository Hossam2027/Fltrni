using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.DTO
{
    public class ProviderSpecParams
    {
        public bool PendingOnly { get; set; } = false;

        public int? CategoryId { get; set; }

        public string? Governorate { get; set; }

        public string? Area { get; set; }

        public string? Search { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
}
