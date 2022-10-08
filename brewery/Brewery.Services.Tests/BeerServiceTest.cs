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
            beerService = new BeerService(mockLogger.Object, mockBeerStorageClient.Object, mockLocalFileRepository.Object);

            beerDetails = Enumerable.Repeat(Mock.Of<BeerDetails>(), 10);
        }

        [TestMethod]
        public async Task Can_get_list_with_data()
        {
            // Assign
            string name = "test";

            mockBeerStorageClient.Setup(x => x.GetList(name)).ReturnsAsync(beerDetails); 

            // Act
            var result = await beerService.GetList(name);

            // Assert
            Assert.AreEqual(10, result.Count);
            mockBeerStorageClient.Verify(m => m.GetList(name), Times.Once);
            mockLocalFileRepository.Verify(m => m.GetAllRatings(), Times.Once);
        }

        [TestMethod]
        public async Task Can_get_empty_list()
        {
            // Assign
            string name = "test";

            mockBeerStorageClient.Setup(x => x.GetList(name)).ReturnsAsync(new List<BeerDetails>());

            // Act
            var result = await beerService.GetList(name);

            // Assert
            Assert.AreEqual(0, result.Count);
            mockBeerStorageClient.Verify(m => m.GetList(name), Times.Once);
            mockLocalFileRepository.Verify(m => m.GetAllRatings(), Times.Once);
        }

        [TestMethod]
        public async Task Can_add_rating_for_valid_id()
        {
            // Assign
            var beerDetails = Mock.Of<BeerDetails>();
            var beerRating = Mock.Of<BeerRating>();
            beerRating.Id = beerDetails.Id;

            mockBeerStorageClient.Setup(m => m.GetBeerDetails(beerDetails.Id)).ReturnsAsync(beerDetails);

            // Act
            await beerService.AddRating(beerRating);

            // Assert
            mockBeerStorageClient.Verify(m => m.GetBeerDetails(beerDetails.Id), Times.Once);
            mockLocalFileRepository.Verify(m => m.AddRating(beerRating), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BeerNotFoundException))]
        public async Task Cannot_add_rating_for_not_valid_id()
        {
            // Assign
            var beerRating = Mock.Of<BeerRating>(); 

            mockBeerStorageClient.Setup(m => m.GetBeerDetails(beerRating.Id)).ThrowsAsync(new BeerNotFoundException());

            // Act
            await beerService.AddRating(beerRating);
        }
    }


}