using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Fltrni.core.Interface;
using Fltrni.core.Entities;
using Fltrni.core.DTO;
namespace Fltrni.core.Specifications
{
    public class ServiceProviderSpecification : BaseSpecification<ServiceProvider>
    {        
        public ServiceProviderSpecification(ProviderSpecParams specParams)
        {
            Criteria = x =>(specParams.PendingOnly ? 
            x.Status == ProviderStatus.Pending: x.Status == ProviderStatus.Approved) &&
           (!specParams.CategoryId.HasValue || x.CategoryId == specParams.CategoryId) &&
           (string.IsNullOrWhiteSpace(specParams.Governorate) || 
           x.Governorate.ToLower() == specParams.Governorate.Trim().ToLower()) &&
           (string.IsNullOrEmpty(specParams.Area) || x.Area == specParams.Area) &&
           (string.IsNullOrEmpty(specParams.Search) || x.Name.Contains(specParams.Search) ||
           x.Governorate.Contains(specParams.Search) || x.Category.Name.Contains(specParams.Search) ||
           x.Area.Contains(specParams.Search));

            //includes
            AddInclude(x => x.Category );
            //sort
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(x => x.Name);
            }
         //paging
         ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
        //specification for categoryid
        public ServiceProviderSpecification(int id): base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
        }
        public class ServiceProviderUserSpecification: BaseSpecification<ServiceProvider>
        {
            public ServiceProviderUserSpecification(string userId): base(p => p.UserId == userId)
            {
            }
        }
    }
}