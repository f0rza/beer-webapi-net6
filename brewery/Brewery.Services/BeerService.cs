using Brewery.Exceptions;
using Brewery.Models;
using Brewery.Models.DTO;
using Brewery.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Brewery.Services
{
    /// <summary>
    /// Handles user rating submission and beer search
    /// </summary>
    public class BeerService : IBeerService
    {
        private readonly ILogger<BeerService> _logger;
        private readonly IBeerStorageClient _beerStorageClient;
        private readonly ILocalFileRepository _localFileRepository;


        public BeerService(ILogger<BeerService> logger, IBeerStorageClient beerStorageClient, ILocalFileRepository localFileRepository)
        {
            _logger = logger;
            _beerStorageClient = beerStorageClient;
            _localFileRepository = localFileRepository;
        }

        /// <summary>
        /// Adds rating after validation
        /// </summary>
        /// <param name="beerRating">beer rating object</param>
        /// <param name="cancellationToken">cancellation token</param>
        public async Task AddRating(BeerRating beerRating, CancellationToken cancellationToken)
        {                        
            _logger.LogDebug("Adding beer rating: {0}", JsonConvert.SerializeObject(beerRating));

            var details = await _beerStorageClient.GetBeerDetails(beerRating.Id, cancellationToken);
            if (details != null)
                await _localFileRepository.AddRating(beerRating, cancellationToken);
            else
                throw new BeerNotFoundException($"Not found {beerRating.Id}");

            _logger.LogDebug("Added beer rating");
        }

        /// <summary>
        /// Gets list of beers along with user ratings
        /// </summary>
        /// <param name="name">search value</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task<IList<BeerDetailsWithRatings>> GetList(string name, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting list of beers by name {0}", name);

            var list = await _beerStorageClient.GetList(name, cancellationToken);
            var ratings = await _localFileRepository.GetAllRatings(cancellationToken);

            var beersWithRatings = list
                .Select(x => new BeerDetailsWithRatings(x, ratings.Where(r => r.Id == x.Id).Select(r => r.Data).ToList()))
                .ToList();

            _logger.LogDebug("Retrieved beers with ratings: {0}", JsonConvert.SerializeObject(beersWithRatings));

            return beersWithRatings;
        }
    }
}