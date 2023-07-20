using System.Linq.Expressions;

namespace DesafioDevApi.Domain.Queries
{
    public abstract class QueryBase
    {
        public static Expression<Func<T, bool>> UpdateParameter<T>(Expression<Func<T, bool>> newParameter, Expression<Func<T, bool>> expression)
        {
            return expression.Combine(newParameter);
        }
    }
}
