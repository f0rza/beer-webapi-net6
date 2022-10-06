using Brewery.Models.DTO;

namespace Brewery.Models
{
    public class BeerRating
    {
        public int Id { get; set; }

        public BeerData Data { get; set; }

        public BeerRating(int id, BeerData data)
        {
            Id = id;
            Data = data;
        }
    }
}
