using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Entities;
using System.Globalization;

namespace DesafioDevApi.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        public async Task<List<Transaction>> ParseCNABFileAsync(IFormFile file)
        {
            var transactions = new List<Transaction>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    var transaction = new Transaction
                    {
                        Type = int.Parse(line.Substring(0, 1)),
                        Date = DateTime.ParseExact(line.Substring(1, 8), "yyyyMMdd", CultureInfo.InvariantCulture),
                        Value = int.Parse(line.Substring(9, 10)) / 100.0m,
                        CPF = line.Substring(19, 11),
                        Card = line.Substring(30, 12),
                        Time = DateTime.Today.Add(TimeSpan.ParseExact(line.Substring(42, 6), "hhmmss", CultureInfo.InvariantCulture)),
                        StoreOwner = line.Substring(48, 14).Trim(),
                        StoreName = line.Substring(62).Trim()
                    };

                    transactions.Add(transaction);
                }
            }

            return transactions;
        }
    }
}
