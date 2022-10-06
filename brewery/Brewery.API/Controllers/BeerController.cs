using Brewery.Exceptions;
using Brewery.Models.DTO;
using Brewery.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Brewery.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly ILogger<BeerController> _logger;
        private readonly IBeerService _beerService;

        public BeerController(ILogger<BeerController> logger, IBeerService beerService)
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
        [HttpPost("AddRating/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddRating([Required][FromRoute] int id,
            [Required][FromBody] BeerData beerData)
        {
            try
            {
                await _beerService.AddRating(new Models.BeerRating(id, beerData));
            }
            catch (BeerNotFoundException be)
            {
                return NotFound(be.Message);
            }

            return Ok();
        }
    }
}