using DesafioDevApi.Domain.Entities;
using DesafioDevApi.Infrastructure.Data.Common;
using DesafioDevApi.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioDevApi.Test
{

    [TestClass]
    public class TransactionRepositoryTest
    {
        private ApiDBContext _dbContext;
        private TransactionRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create in-memory database
            var options = new DbContextOptionsBuilder<ApiDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApiDBContext(options);
            _repository = new TransactionRepository(_dbContext);
        }
        private Transaction TransactionFake()
        {
            return new Transaction
            {
                Type = 1,
                Date = new DateTime(2023, 7, 20),
                Value = 100.0m,
                CPF = "12345678901",
                Card = "123456789012",
                Time = new DateTime(2023, 7, 20, 12, 0, 0),
                StoreOwner = "Test Owner",
                StoreName = "Test Store"
            };
        }
        [TestMethod]
        public async Task TestAddTransaction()
        {
            // Arrange
            var transaction = TransactionFake();

            // Act
            await _repository.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            // Assert
            Assert.AreEqual(1, _dbContext.Transactions.Count());
        }
        [TestMethod]
        public async Task TestGetByIdTransaction()
        {
            // Arrange
            var transaction = TransactionFake();
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync<int>(x => x.Id == transaction.Id);

            // Assert
            Assert.AreEqual(transaction.Id, result.Id);
            Assert.AreEqual(transaction.Type, result.Type);
            Assert.AreEqual(transaction.Date, result.Date);
            Assert.AreEqual(transaction.Value, result.Value);
            Assert.AreEqual(transaction.CPF, result.CPF);
            Assert.AreEqual(transaction.Card, result.Card);
            Assert.AreEqual(transaction.Time, result.Time);
            Assert.AreEqual(transaction.StoreOwner, result.StoreOwner);
            Assert.AreEqual(transaction.StoreName, result.StoreName);
        }
        [TestMethod]
        public async Task TestGetAllTransaction()
        {
            // Arrange
            var transaction = TransactionFake();
            var transactionAlternate = TransactionFake();
            // Act
            await _repository.AddAsync(transaction);
            await _repository.AddAsync(transactionAlternate);
            await _dbContext.SaveChangesAsync();

            // Assert
            Assert.IsTrue(_dbContext.Transactions.Count() > 1);
        }
    }
}