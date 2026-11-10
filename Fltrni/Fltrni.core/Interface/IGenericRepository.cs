using Fltrni.core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification);
        Task AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task<int> CountAsync(ISpecification<T> specification);
    }
}
