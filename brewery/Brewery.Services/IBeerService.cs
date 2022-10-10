using Brewery.Models;
using Brewery.Models.DTO;

namespace Brewery.Services
{
    public interface IBeerService
    {
        Task AddRating(BeerRating beerRating, CancellationToken cancellationToken);
        Task<IList<BeerDetailsWithRatings>> GetList(string name, CancellationToken cancellationToken);
    }
}