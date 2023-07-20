using DesafioDevApi.Domain.Commands.Inputs;
using System.Linq.Expressions;

namespace DesafioDevApi.Domain.Queries
{
    public class TransactionGetCreateQueryCondition : QueryBase
    {
        public static Expression<Func<Entities.Transaction, bool>> CreateQueryCondition(TransactionGetRequestCommand search)
        {
            Expression<Func<Entities.Transaction, bool>> expression = default;
            Expression<Func<Entities.Transaction, bool>> expressionId = x => x.Id == search.Id;
            if (search.Id != default)
                expression = expressionId;

            return expression;
        }
    }
}
