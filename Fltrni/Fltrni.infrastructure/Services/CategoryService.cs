using Fltrni.core.DTO;
using Fltrni.core.Entities;
using Fltrni.core.Interface;
using Fltrni.infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _unitOfWork.Repository<Category>().AddAsync(category);

            await _unitOfWork.CompleteAsync();
        }
        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.CompleteAsync();
        }

        //public async Task<IReadOnlyList<string>> GetAreasAsync(int? categoryId,string? governorate)
        //    {
        //        var providers = await _unitOfWork.Repository<ServiceProvider>() .GetAllAsync();

        //        return providers
        //            .Where(p => p.Status == ProviderStatus.Approved)
        //            .Where(p => !categoryId.HasValue || categoryId == 0
        //                        || p.CategoryId == categoryId)
        //            .Where(p => string.IsNullOrEmpty(governorate)
        //                        || p.Governorate == governorate)
        //            .Select(p => p.Area)
        //            .Where(area => !string.IsNullOrWhiteSpace(area))
        //            .Select(area => area.Trim())
        //            .Distinct()
        //            .OrderBy(area => area)
        //            .ToList();
        //    }
        //}
    }
}
   
