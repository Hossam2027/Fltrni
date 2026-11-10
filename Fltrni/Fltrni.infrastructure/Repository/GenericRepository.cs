using Fltrni.core.Interface;
using Fltrni.core.Specifications;
using Fltrni.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
            return await query.ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
                   

        public void Update(T entity)
        {
        _context.Set<T>().Update(entity);

        }
        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            var query = _context.Set<T>().AsQueryable();

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            return await query.CountAsync();
        }
    }
}
