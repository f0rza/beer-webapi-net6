using Brewery.Exceptions;
using Brewery.Models;
using Brewery.Models.DTO;
using Brewery.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace Brewery.Services.Tests
{
    [TestClass]
    public class BeerServiceTest
    {
        private Mock<ILogger<BeerService>> mockLogger;
        private Mock<IBeerStorageClient> mockBeerStorageClient;
        private Mock<ILocalFileRepository> mockLocalFileRepository;
        private CancellationToken cancellationToken;
        private BeerService beerService;

        private IEnumerable<BeerDetails> beerDetails = new List<BeerDetails>();

        [TestInitialize]
        public void Init()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            mockLogger = new Mock<ILogger<BeerService>>();
            mockBeerStorageClient = new Mock<IBeerStorageClient>();
            mockLocalFileRepository = new Mock<ILocalFileRepository>();
            cancellationToken = new CancellationToken();
            beerService = new BeerService(mockLogger.Object, mockBeerStorageClient.Object, mockLocalFileRepository.Object);
            
            beerDetails = Enumerable.Repeat(Mock.Of<BeerDetails>(), 10);
        }

        [TestMethod]
        public async Task Can_get_list_with_data()
        {
            // Assign
            string name = "test";

            mockBeerStorageClient.Setup(x => x.GetList(name, new CancellationToken())).ReturnsAsync(beerDetails); 

            // Act
            var result = await beerService.GetList(name, cancellationToken);

            // Assert
            Assert.AreEqual(10, result.Count);
            mockBeerStorageClient.Verify(m => m.GetList(name, cancellationToken), Times.Once);
            mockLocalFileRepository.Verify(m => m.GetAllRatings(cancellationToken), Times.Once);
        }

        [TestMethod]
        public async Task Can_get_empty_list()
        {
            // Assign
            string name = "test";

            mockBeerStorageClient.Setup(x => x.GetList(name, cancellationToken)).ReturnsAsync(new List<BeerDetails>());

            // Act
            var result = await beerService.GetList(name, cancellationToken);

            // Assert
            Assert.AreEqual(0, result.Count);
            mockBeerStorageClient.Verify(m => m.GetList(name, cancellationToken), Times.Once);
            mockLocalFileRepository.Verify(m => m.GetAllRatings(cancellationToken), Times.Once);
        }

        [TestMethod]
        public async Task Can_add_rating_for_valid_id()
        {
            // Assign
            var beerDetails = Mock.Of<BeerDetails>();
            var beerRating = Mock.Of<BeerRating>();
            beerRating.Id = beerDetails.Id;

            mockBeerStorageClient.Setup(m => m.GetBeerDetails(beerDetails.Id, cancellationToken)).ReturnsAsync(beerDetails);

            // Act
            await beerService.AddRating(beerRating, cancellationToken);

            // Assert
            mockBeerStorageClient.Verify(m => m.GetBeerDetails(beerDetails.Id, cancellationToken), Times.Once);
            mockLocalFileRepository.Verify(m => m.AddRating(beerRating, cancellationToken), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BeerNotFoundException))]
        public async Task Cannot_add_rating_for_not_valid_id()
        {
            // Assign
            var beerRating = Mock.Of<BeerRating>(); 

            mockBeerStorageClient.Setup(m => m.GetBeerDetails(beerRating.Id, cancellationToken)).ThrowsAsync(new BeerNotFoundException());

            // Act
            await beerService.AddRating(beerRating, cancellationToken);
        }
    }
}