using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Brewery.API.ActionFilters;
using Moq;
using Brewery.Models.DTO;

namespace Brewery.API.Tests
{
    [TestClass]
    public class ModelValidationErrorHandlerFilterAttributeTests
    {
        private ActionExecutingContext actionExecutingContext;
        private ModelValidationErrorHandlerFilterAttribute actionFilter = new ModelValidationErrorHandlerFilterAttribute();
        
        [TestInitialize]
        public void Init()
        {
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext,
                new RouteData(),
                new ActionDescriptor(),
                new ModelStateDictionary());
            
            actionExecutingContext = new ActionExecutingContext(actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controller: null);
        }

        [TestMethod]
        public async Task Valid_username_must_pass_validation()
        {
            // Arrange            
            actionExecutingContext.ActionArguments.Clear();
            actionExecutingContext.ActionArguments.Add("beerData", new BeerUserRating { Username = "test@ua.fm" });

            ActionExecutionDelegate d = Mock.Of<ActionExecutionDelegate>();

            // Act
            await actionFilter.OnActionExecutionAsync(actionExecutingContext, d);

            // Assert            
            Assert.IsTrue(actionExecutingContext.ModelState.IsValid);
        }

        [TestMethod]
        public async Task Valid_username_must_fail_validation()
        {
            // Arrange            
            actionExecutingContext.ActionArguments.Clear();
            actionExecutingContext.ActionArguments.Add("beerData", new BeerUserRating { Username = "test" });

            ActionExecutionDelegate d = Mock.Of<ActionExecutionDelegate>();

            // Act
            await actionFilter.OnActionExecutionAsync(actionExecutingContext, d);

            // Assert            
            Assert.IsFalse(actionExecutingContext.ModelState.IsValid);
        }
    }
}