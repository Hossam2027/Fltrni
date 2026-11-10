using Fltrni.core.DTO;
using Fltrni.core.Entities;
using Fltrni.core.Interface;
using Fltrni.core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateAsync(CreateReviewDto dto, string customerId)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                return "Rating must be between 1 and 5";

            var provider = await _unitOfWork.Repository<ServiceProvider>().GetByIdAsync(dto.ServiceProviderId);

            if (provider == null)
                return "Service Provider not found";

            if (provider.Status != ProviderStatus.Approved)
                return "You can only review an approved service provider";

            var review = new Review
            {
                ServiceProviderId = dto.ServiceProviderId,
                CustomerId = customerId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                Created = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Review>().AddAsync(review);

            await _unitOfWork.CompleteAsync();


            var spec = new ReviewSpecification(dto.ServiceProviderId);

            var reviews = await _unitOfWork.Repository<Review>().GetAllAsync(spec);

            // 7. Calculate Rating and ReviewsCount
            provider.ReviewsCount = reviews.Count;

            provider.Rating = reviews.Any()? reviews.Average(x => x.Rating): 0;

            // 8. Update Provider
            _unitOfWork.Repository<ServiceProvider>().Update(provider);

            // 9. Save changes
            await _unitOfWork.CompleteAsync();
            return "Review created successfully";

        }
        public async Task<IReadOnlyList<ReviewDto>> GetByProviderAsync(int providerId)
        {
            var spec = new ReviewSpecification(providerId);
            var reviews = await _unitOfWork.Repository<Review>().GetAllAsync(spec);
            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                Created = r.Created
            }).ToList();
        }
    }

    }

