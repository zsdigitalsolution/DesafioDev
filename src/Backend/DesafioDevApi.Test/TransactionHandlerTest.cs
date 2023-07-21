using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Entities;
using DesafioDevApi.Domain.Handlers;
using DesafioDevApi.Infrastructure.Data.Common;
using DesafioDevApi.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DesafioDevApi.Test
{
    [TestClass]
    public class TransactionHandlerTests
    {
        private Mock<ITransactionService> _serviceMock;
        private ApiDBContext _dbContext;
        private TransactionRepository _repository;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private TransactionFileHandler _fileHandler;
        private TransactionGetAllHandler _getAllHandler;
        private TransactionGetHandler _getHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceMock = new Mock<ITransactionService>();
            // Create in-memory database
            var options = new DbContextOptionsBuilder<ApiDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApiDBContext(options);
            _repository = new TransactionRepository(_dbContext);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _fileHandler = new TransactionFileHandler(_repository, _serviceMock.Object, _unitOfWorkMock.Object);
            _getAllHandler = new TransactionGetAllHandler(_repository);
            _getHandler = new TransactionGetHandler(_repository);
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
        public async Task TestTransactionFileHandler()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var content = "1201903010000014200096206760174753***3153153453JOÃO MACEDO   BAR DO JOÃO       ";
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            var file = fileMock.Object;

            var request = new TransactionFileRequestCommand(file: file);
            var transactions = new List<Transaction> { TransactionFake(), TransactionFake() };
            _serviceMock.Setup(s => s.ParseCNABFileAsync(request.File)).ReturnsAsync(transactions);
            await _repository.AddAsync(transactions);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await _fileHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(!response.HasMessages);
        }

        [TestMethod]
        public async Task TestTransactionGetAllHandler()
        {
            // Preparação
            var request = new TransactionGetAllRequestCommand();
            var transactions = new List<Transaction> {  TransactionFake(), TransactionFake() };

            await _repository.AddAsync(transactions);
            await _dbContext.SaveChangesAsync();

            // Ação
            var response = await _getAllHandler.Handle(request, CancellationToken.None);

            // Verificação
            Assert.IsTrue(!response.HasMessages);
            Assert.AreEqual(transactions.Count, ((IEnumerable<TransactionResponseCommand>)response.Value).Count());
        }



        [TestMethod]
        public async Task TestTransactionGetHandler()
        {
            // Arrange            
            var transaction = TransactionFake();
            var request = new TransactionGetRequestCommand(id: transaction.Id);
            await _repository.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await _getHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(!response.HasMessages);
            Assert.AreEqual(transaction.Id, ((TransactionResponseCommand)response.Value).Id);
        }
    }
}
