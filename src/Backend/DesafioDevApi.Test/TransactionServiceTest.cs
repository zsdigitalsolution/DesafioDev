using DesafioDevApi.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace DesafioDevApi.Test
{
    [TestClass]
    public class TransactionServiceTest
    {
        private TransactionService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new TransactionService();
        }

        [TestMethod]
        public async Task TestParseCNABFileAsync()
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

            // Act
            var result = await _service.ParseCNABFileAsync(file);

            // Assert
            Assert.AreEqual(1, result.Count);
            var transaction = result.First();
            Assert.AreEqual(1, transaction.Type);
            Assert.AreEqual(new DateTime(2019, 03, 01), transaction.Date);
            Assert.AreEqual(142.0m, transaction.Value);
            Assert.AreEqual("09620676017", transaction.CPF);
            Assert.AreEqual("4753****3153", transaction.Card);
            Assert.AreEqual(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 34, 53), transaction.Time);
            Assert.AreEqual("JOÃO MACEDO", transaction.StoreOwner);
            Assert.AreEqual("BAR DO JOÃO", transaction.StoreName);
        }
    }
}