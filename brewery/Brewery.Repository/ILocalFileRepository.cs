using Brewery.Models;

namespace Brewery.Repositories
{
    public interface ILocalFileRepository
    {
        Task AddRating(BeerRating beerRating);
    }
}