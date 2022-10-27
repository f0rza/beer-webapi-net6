using Brewery.Models.DTO;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace Brewery.API.ActionFilters
{
    /// <summary>
    /// Validates email in a model   
    /// </summary>
    /// <remarks>I'd use regex attribute on BeerData.Username for this validation which will generate consistent error output as for Rating. But for showing work with ActionFilterAttribute it's ok.</remarks>
    public class ModelValidationErrorHandlerFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var beerData = context.ActionArguments["beerData"] as BeerUserRating;
            if (beerData != null)
            {
                var regex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
                if (!regex.IsMatch(beerData.Username))
                {
                    context.ModelState.AddModelError(nameof(beerData.Username), "Value is not a valid email address.");
                }
            }

            await next();
        }
    }
}
