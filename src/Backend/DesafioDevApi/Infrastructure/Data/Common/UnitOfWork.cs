using DesafioDevApi.Domain.Contract;

namespace DesafioDevApi.Infrastructure.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDBContext _dbContext;
        public ITransactionRepository Transactions { get; }

        public UnitOfWork(ApiDBContext dbContext,
                            ITransactionRepository transactionRepository)
        {
            _dbContext = dbContext;
            Transactions = transactionRepository;
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
