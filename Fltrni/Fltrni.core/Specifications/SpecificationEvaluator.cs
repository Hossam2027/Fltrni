using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Fltrni.core.Specifications
{
    public  class SpecificationEvaluator <T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
           
            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }
          
            if (spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
            }
            foreach (var include in spec.Includes)
            {
                inputQuery = inputQuery.Include(include);
            }
            if (spec.Skip.HasValue)
            {
                inputQuery = inputQuery.Skip(spec.Skip.Value);
            }
            if (spec.Take.HasValue)
            {
                inputQuery = inputQuery.Take(spec.Take.Value);
            }
            return inputQuery;
        }   
    }
}
