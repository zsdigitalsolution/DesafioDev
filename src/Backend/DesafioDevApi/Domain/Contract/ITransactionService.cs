using DesafioDevApi.Domain.Entities;

namespace DesafioDevApi.Domain.Contract
{
    public interface ITransactionService
    {
        Task<List<Transaction>> ParseCNABFileAsync(IFormFile file);
    }
}
