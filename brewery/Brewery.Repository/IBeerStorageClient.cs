using Brewery.Models.DTO;

namespace Brewery.Repositories
{
    public interface IBeerStorageClient
    {
        Task<BeerDetails> GetBeerDetails(int id);
    }
}