using Brewery.Models;
using Brewery.Repositories;

namespace Brewery.Services
{
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
    }
}