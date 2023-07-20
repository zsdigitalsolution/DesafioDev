using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Entities;
using DesafioDevApi.Infrastructure.Data.Common;

namespace DesafioDevApi.Infrastructure.Data.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApiDBContext dbContext) : base(dbContext)
        {

        }
    }
}
