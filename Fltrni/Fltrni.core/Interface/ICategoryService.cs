using Fltrni.core.DTO;
using Fltrni.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto dto);
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);

    }
}
