using Moq;
using Brewery.API.Controllers;
using Brewery.Services;
using Microsoft.Extensions.Logging;
using Brewery.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Brewery.API.Tests
{
    /// <summary>
    /// Beer controller tests
    /// </summary>
    /// <remarks>
    /// A couple of tests added here. Additionally can be added test for breaking request using cancellation token.
    /// AddRating not tested at all at the moment: potential tests - returning bad requests when model validation fails, success 201, not found 404, problem 500 tests, corresponding logging etc
    /// </remarks>
    [TestClass]    
    public class BeersControllerTests
    {
        private Mock<ILogger<BeersController>> mockLogger;
        private Mock<IBeerService> mockBeerService;
        private BeersController controller;
        private CancellationToken cancellationToken = new CancellationTokenSource().Token;

        [TestInitialize]
        public void Init()
        {
            mockLogger = new Mock<ILogger<BeersController>>();
            mockBeerService = new Mock<IBeerService>();
        }

        [TestMethod]
        public async Task Can_get_list_of_beers()
        {
            // Arrange
            mockBeerService.Setup(x => x.GetList(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<BeerDetailsWithRatings> { Mock.Of<BeerDetailsWithRatings>() });
            controller = new BeersController(mockLogger.Object, mockBeerService.Object);

            // Act
            var actionResult = await controller.GetList("test", cancellationToken);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<BeerDetailsWithRatings>));
            Assert.AreEqual(((List<BeerDetailsWithRatings>)result.Value).Count(), 1);
        }

        [TestMethod]
        public async Task Get_list_can_return_problem()
        {
            // Arrange
            mockBeerService.Setup(x => x.GetList(It.IsAny<string>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());
            controller = new BeersController(mockLogger.Object, mockBeerService.Object);

            // Act
            var actionResult = await controller.GetList("test", cancellationToken);

            // Assert
            var result = actionResult.Result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(ProblemDetails));
            Assert.AreEqual(((ProblemDetails)result.Value).Status, (int)HttpStatusCode.InternalServerError);
        }
    }
}