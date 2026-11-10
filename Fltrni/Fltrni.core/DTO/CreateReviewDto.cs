using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.DTO
{
    public  class CreateReviewDto
    {
        public int ServiceProviderId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
