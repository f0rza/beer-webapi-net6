using Brewery.API.ActionFilters;
using Brewery.Exceptions;
using Brewery.Models;
using Brewery.Models.DTO;
using Brewery.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Brewery.API.Controllers
{
    /// <summary>
    /// API endpoints that allows to get list of beers and submit user rating
    /// </summary>
    /// <remarks>used Microsoft recommendations for Restful service naming conventions: https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design</remarks>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeersController : ControllerBase
    {
        private readonly ILogger<BeersController> _logger;
        private readonly IBeerService _beerService;

        public BeersController(ILogger<BeersController> logger, IBeerService beerService)
        {
            _logger = logger;
            _beerService = beerService;
        }

        /// <summary>
        /// Adds rating to the beer
        /// </summary>
        /// <param name="id">valid beer id</param>
        /// <param name="beerData">username, rating (1 to 5) and comments</param>
        /// <returns></returns>
        [HttpPost("{id}/rate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ModelValidationErrorHandlerFilter]
        public async Task<ActionResult> AddRating([Required][FromRoute] int id,
            [Required][FromBody] BeerData beerData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var beerRating = new BeerRating(id, beerData);

                await _beerService.AddRating(beerRating);

            }
            catch (BeerNotFoundException be)
            {
                _logger.LogWarning(be.Message);
                return NotFound(be.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }

            return Created(string.Empty, null);
        }

        /// <summary>
        /// Gets list of matching beers along with user ratings
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<BeerDetailsWithRatings>>> GetList([Required][FromRoute] string name)
        {
            try
            {
                var list = await _beerService.GetList(name);

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}