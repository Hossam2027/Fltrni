using Fltrni.core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface IReviewService
    {
        Task<string> CreateAsync(CreateReviewDto dto, string customerId);
        Task<IReadOnlyList<ReviewDto>> GetByProviderAsync(int providerId);
    }
}
