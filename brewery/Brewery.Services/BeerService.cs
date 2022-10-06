using Brewery.Models;
using Brewery.Models.DTO;
using Brewery.Repositories;

namespace Brewery.Services
{
    /// <summary>
    /// Handles user rating submission and beer search
    /// </summary>
    public class BeerService : IBeerService
    {
        private readonly IBeerStorageClient _beerStorageClient;
        private readonly ILocalFileRepository _localFileRepository;

        public BeerService(IBeerStorageClient beerStorageClient, ILocalFileRepository localFileRepository)
        {
            _beerStorageClient = beerStorageClient;
            _localFileRepository = localFileRepository;
        }

        /// <summary>
        /// Adds rating after validation
        /// </summary>
        /// <param name="beerRating"></param>
        public async Task AddRating(BeerRating beerRating)
        {
            var details = await _beerStorageClient.GetBeerDetails(beerRating.Id);
            if (details != null)
                await _localFileRepository.AddRating(beerRating);
        }

        /// <summary>
        /// Gets list of beers along with user ratings
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IList<BeerDetailsWithRatings>> GetList(string name)
        {
            var list = await _beerStorageClient.GetList(name);
            var ratings = await _localFileRepository.GetAllRatings();

            return list.Select(x => new BeerDetailsWithRatings(x, ratings.Where(r => r.Id == x.Id).Select(r => r.Data).ToList())).ToList();
        }
    }
}