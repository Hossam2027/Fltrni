using System.Linq.Expressions;

namespace Fltrni.core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {              
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification()
        {
        }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        protected void ApplyPaging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
        }
        public Expression<Func<T, bool>>? Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }= [];

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public Expression<Func<T, object>>? OrderByDescending { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

    }
}
