using Fltrni.core.DTO;
using Fltrni.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface IServiceProviderService
    {
        Task<Pagination<ServiceProviderDto>> GetallAsync(ProviderSpecParams specParams);
        Task CreateServiceProviderAsync(CreateServiceProviderDto createServiceProviderDto, string userId);
        Task<ServiceProvider?> GetMyStatusAsync(string userId);
        Task<string> ApproveAsync(int id);
        Task<string> RejectAsync(int id);
        Task<string> DeleteAsync(int id);
        Task<ServiceProviderDto?> GetserviceProviderByIdAsync(int id);
        Task UpdateAsync(int id, UpdateServiceProviderDto dto);
        Task <IReadOnlyList<object>> GetAreasAsync(int? categoryId, string? governorate);
    }
}
