using Brewery.Models;

namespace Brewery.Services
{
    public interface IBeerService
    {
        Task AddRating(BeerRating beerRating);
    }
}