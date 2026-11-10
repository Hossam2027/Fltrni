using Fltrni.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Specifications
{
    public class ReviewSpecification : BaseSpecification<Review>
    {
        public ReviewSpecification(int serviceProviderId): base(x => x.ServiceProviderId == serviceProviderId)
        {
        }
    }
}
