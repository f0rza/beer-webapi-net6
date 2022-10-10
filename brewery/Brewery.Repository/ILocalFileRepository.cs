using Brewery.Models;

namespace Brewery.Repositories
{
    public interface ILocalFileRepository
    {
        Task AddRating(BeerRating beerRating, CancellationToken cancellationToken);
        Task<IEnumerable<BeerRating>> GetAllRatings(CancellationToken cancellationToken);
    }
}