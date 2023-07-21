using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Entities;
using DesafioDevApi.Domain.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace DesafioDevApi.Test
{
    [TestClass]
    public class TransactionHandlerTests
    {
        private Mock<ITransactionService> _serviceMock;
        private Mock<ITransactionRepository> _repositoryMock;       
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private TransactionFileHandler _fileHandler;
        private TransactionGetAllHandler _getAllHandler;
        private TransactionGetHandler _getHandler;

        [TestInitialize]
        public void TestInitialize()
        {    _serviceMock = new Mock<ITransactionService>();
            _repositoryMock = new Mock<ITransactionRepository>();
            
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _fileHandler = new TransactionFileHandler(_repositoryMock.Object, _serviceMock.Object, _unitOfWorkMock.Object);
            _getAllHandler = new TransactionGetAllHandler(_repositoryMock.Object);
            _getHandler = new TransactionGetHandler(_repositoryMock.Object);
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
            var content = "1201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO       ";
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
            _repositoryMock.Setup(r => r.AddAsync(transactions)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.Commit()).Returns(true);

            // Act
            var response = await _fileHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(!response.HasMessages);
        }

        [TestMethod]
        public async Task TestTransactionGetAllHandler()
        {
            // Arrange
            var request = new TransactionGetAllRequestCommand();
            var transactions = new List<Transaction> { TransactionFake(), TransactionFake() };
            _repositoryMock.Setup(r => r.AddAsync(transactions)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.Commit()).Returns(true);
            
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var response = await _getAllHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(!response.HasMessages);
            Assert.AreEqual(transactions.Count, ((IEnumerable<TransactionResponseCommand>)response.Value).Count());
        }

        [TestMethod]
        public async Task TestTransactionGetHandler()
        {
            // Arrange
            var request = new TransactionGetRequestCommand(id: 1);
            var transaction = TransactionFake();
            _repositoryMock.Setup(r => r.GetByIdAsync<Transaction>(x => x.Id == request.Id, null, false)).ReturnsAsync(transaction);

            // Act
            var response = await _getHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(!response.HasMessages);
            Assert.AreEqual(transaction.Id, ((TransactionResponseCommand)response.Value).Id);
        }


    }
}