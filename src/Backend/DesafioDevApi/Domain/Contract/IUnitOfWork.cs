namespace DesafioDevApi.Domain.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository Transactions { get; }

        bool Commit();
    }
}
