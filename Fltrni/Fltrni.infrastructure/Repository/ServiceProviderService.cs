using Fltrni.core.DTO;
using Fltrni.core.Entities;
using Fltrni.core.Exceptions;
using Fltrni.core.Interface;
using Fltrni.core.Specifications;
using Fltrni.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fltrni.core.Specifications.ServiceProviderSpecification;

namespace Fltrni.infrastructure.Repository
{
    public class ServiceProviderService : IServiceProviderService

    {
        public IUnitOfWork UnitOfWork { get; }
        public UserManager<AppUser> UserManager { get; }
        public ServiceProviderService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
        }

        public async Task<Pagination<ServiceProviderDto>> GetallAsync(ProviderSpecParams specParams)
        {
            var spec = new ServiceProviderSpecification(specParams);

            // إجمالي النتائج المطابقة للفلاتر، بدون Skip وTake
            var count = await UnitOfWork.Repository<ServiceProvider>().CountAsync(spec);

            // بيانات الصفحة الحالية فقط
            var serviceProviders = await UnitOfWork.Repository<ServiceProvider>().GetAllAsync(spec);

            var data = serviceProviders.Select(sp => new ServiceProviderDto
            {
                Id = sp.Id,
                Name = sp.Name,
                Phone = sp.Phone,
                Governorate = sp.Governorate,
                Area = sp.Area,
                Address = sp.Address,
                StartingPrice = sp.StartingPrice,
                Rating = sp.Rating,
                Latitude = sp.Latitude?.ToString(),
                Longitude = sp.Longitude?.ToString(),
                ReviewsCount = sp.ReviewsCount,
                CategoryId = sp.CategoryId,
                CategoryName = sp.Category.Name,
                DateCreated = DateTime.Now,
                GoogleMapsUrl = sp.Latitude.HasValue && sp.Longitude.HasValue ?
                $"https://www.google.com/maps/dir/?api=1&destination={sp.Latitude},{sp.Longitude}&travelmode=driving" : null
            }).ToList();

            return new Pagination<ServiceProviderDto>
            {
                PageIndex = specParams.PageIndex,
                PageSize = specParams.PageSize,
                Count = count,
                Data = data
            };
        }

        public async Task CreateServiceProviderAsync(CreateServiceProviderDto createServiceProviderDto, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var email = user.Email;

            //var existingProviderSpec = new ServiceProviderEmailSpecification(email);

            //var existingProviders = await UnitOfWork.Repository<ServiceProvider>().GetAllAsync(existingProviderSpec);

            Category? category = null;

            // Case 1: The user entered a new category name
            if (!string.IsNullOrWhiteSpace(createServiceProviderDto.NewCategoryName))
            {
                var categoryName = createServiceProviderDto.NewCategoryName.Trim();

                var categories = await UnitOfWork.Repository<Category>().GetAllAsync();

                category = categories.FirstOrDefault(c => c.Name.Trim().Equals(categoryName, StringComparison.OrdinalIgnoreCase));

                if (category is null)
                {
                    category = new Category
                    {
                        Name = categoryName
                    };

                    await UnitOfWork.Repository<Category>()
                        .AddAsync(category);
                }
            }
            // Case 2: The user selected an existing category
            else
            {
                if (createServiceProviderDto.CategoryId <= 0)
                    throw new BadRequestException(
                        "Please select a valid category.");

                category = await UnitOfWork.Repository<Category>()
                    .GetByIdAsync(createServiceProviderDto.CategoryId);

                if (category is null)
                    throw new BadRequestException(
                        "Category not found.");
            }


            var provider = new ServiceProvider
            {
                Name = createServiceProviderDto.Name,
                Phone = createServiceProviderDto.Phone,
                Email = email,
                UserId = userId,
                Governorate = createServiceProviderDto.Governorate,
                Area = createServiceProviderDto.Area,
                Address = createServiceProviderDto.Address,
                StartingPrice = createServiceProviderDto.StartingPrice,
                Latitude = createServiceProviderDto.Latitude,
                Longitude = createServiceProviderDto.Longitude,

                // Link the provider to the selected or new category
                Category = category
            };

            await UnitOfWork.Repository<ServiceProvider>().AddAsync(provider);

            await UnitOfWork.CompleteAsync();
        }

        public async Task<string> ApproveAsync(int id)
        {
            var provider = await UnitOfWork.Repository<ServiceProvider>().GetByIdAsync(id);
            if (provider is null) return ("Provider not found");


            provider.Status = ProviderStatus.Approved;
            UnitOfWork.Repository<ServiceProvider>().Update(provider);
            await UnitOfWork.CompleteAsync();
            if (!string.IsNullOrEmpty(provider.UserId))
            {
                var user = await UserManager.FindByIdAsync(provider.UserId);

                if (user is not null)
                {
                    var isProvider = await UserManager.IsInRoleAsync(user, "Provider");

                    if (!isProvider)
                    {
                        await UserManager.AddToRoleAsync(user, "Provider");
                    }
                }
            }
            return ("Service Provider approved successfully");
        }

        public async Task<string> RejectAsync(int id)
        {
            var provider = await UnitOfWork.Repository<ServiceProvider>().GetByIdAsync(id);
            if (provider is null) return ("Provider not found");


            provider.Status = ProviderStatus.Rejected;
            UnitOfWork.Repository<ServiceProvider>().Update(provider);
            await UnitOfWork.CompleteAsync();
            return ("Service Provider rejected successfully");
        }
        public async Task<string> DeleteAsync(int id)
        {
            var provider = await UnitOfWork.Repository<ServiceProvider>().GetByIdAsync(id);
            if (provider is null) return ("Provider not found");
            UnitOfWork.Repository<ServiceProvider>().Delete(provider);
            await UnitOfWork.CompleteAsync();
            return ("Service Provider deleted successfully");
        }
        public async Task<ServiceProviderDto?> GetserviceProviderByIdAsync(int id)
        {

            var provider = await UnitOfWork.Repository<ServiceProvider>().GetByIdAsync(id);
            if (provider is null)
                return null;
            return new ServiceProviderDto

            {
                Id = provider.Id,
                Name = provider.Name,
                Phone = provider.Phone,
                Governorate = provider.Governorate,
                Area = provider.Area,
                Address = provider.Address,
                StartingPrice = provider.StartingPrice,
                Rating = provider.Rating,
                Latitude = provider.Latitude?.ToString(),
                Longitude = provider.Longitude?.ToString(),
                ReviewsCount = provider.ReviewsCount,
                CategoryId = provider.CategoryId,
                email = provider.Email,
                DateCreated = DateTime.Now,
            };
        }
        public async Task UpdateAsync(int id, UpdateServiceProviderDto dto)
        {
            var provider = await UnitOfWork.Repository<ServiceProvider>().GetByIdAsync(id);
            if (provider is null) throw new Exception("Provider not found");
            provider.Name = dto.Name;
            provider.Phone = dto.Phone;
            provider.Email = dto.Email;
            provider.Governorate = dto.Governorate;
            provider.Area = dto.Area;
            provider.Address = dto.Address;
            provider.StartingPrice = dto.StartingPrice;
            provider.Latitude = dto.Latitude;
            provider.Longitude = dto.Longitude;
            provider.CategoryId = dto.CategoryId;
            UnitOfWork.Repository<ServiceProvider>().Update(provider);
            await UnitOfWork.CompleteAsync();
        }

        public async Task<ServiceProvider?> GetMyStatusAsync(string userId)
        {
            var spec = new ServiceProviderUserSpecification(userId);

            var providers = await UnitOfWork.Repository<ServiceProvider>().GetAllAsync(spec);

            return providers.FirstOrDefault();
        }
        public async Task<IReadOnlyList<object>> GetAreasAsync(int? categoryId, string? governorate)
        {
            var providers = await UnitOfWork.Repository<ServiceProvider>().GetAllAsync();

            return providers
                .Where(p => p.Status == ProviderStatus.Approved)
                .Where(p => !categoryId.HasValue || categoryId == 0
                            || p.CategoryId == categoryId)
                .Where(p => string.IsNullOrEmpty(governorate)
                            || p.Governorate == governorate)
                .Select(p => p.Area)
                .Where(area => !string.IsNullOrWhiteSpace(area))
                .Select(area => area.Trim())
                .Distinct()
                .OrderBy(area => area)
                .ToList();
        }
    }
}

