using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Entities;
using DesafioDevApi.Domain.Handlers;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;

namespace DesafioDevApi.Test
{
    [TestClass]
    public class TransactionHandlerTest
    {
        private Mock<ITransactionService> _mockService;
        private Mock<ITransactionRepository> _mockRepository;
        private TransactionFileHandler _fileHandler;
        private TransactionGetAllHandler _getAllHandler;
        private TransactionGetHandler _getHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockService = new Mock<ITransactionService>();
            _mockRepository = new Mock<ITransactionRepository>();
            _fileHandler = new TransactionFileHandler(_mockService.Object);
            _getAllHandler = new TransactionGetAllHandler(_mockRepository.Object);
            _getHandler = new TransactionGetHandler(_mockRepository.Object);
        }

        [TestMethod]
        public async Task TestTransactionFileHandler()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var transactions = new List<Transaction> { new Transaction(), new Transaction() };
            _mockService.Setup(service => service.ParseCNABFileAsync(fileMock.Object)).ReturnsAsync(transactions);

            // Act
            var result = await _fileHandler.Handle(fileMock.Object, CancellationToken.None);

            // Assert
            Assert.AreEqual(transactions.Count, result.Count());
        }

        [TestMethod]
        public async Task TestTransactionGetAllHandler()
        {
            // Arrange
            var transactions = new List<Transaction> { new Transaction(), new Transaction() };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var result = await _getAllHandler.Handle(new TransactionGetAllQuery(), CancellationToken.None);

            // Assert
            Assert.AreEqual(transactions.Count, result.Count());
        }

        [TestMethod]
        public async Task TestTransactionGetHandler()
        {
            // Arrange
            var transaction = new Transaction();
            _mockRepository.Setup(repo => repo.GetByIdAsync<Transaction>(It.IsAny<Expression<Func<Transaction, bool>>>())).ReturnsAsync(transaction);

            // Act
            var result = await _getHandler.Handle(new TransactionGetQuery { Id = 1 }, CancellationToken.None);

            // Assert
            Assert.AreEqual(transaction, result);
        }
    }
}