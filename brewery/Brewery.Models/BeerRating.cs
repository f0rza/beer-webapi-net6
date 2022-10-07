using Brewery.Models.DTO;

namespace Brewery.Models
{
    /// <summary>
    /// Class implements record for JSON storage
    /// </summary>
    public class BeerRating
    {
        public int Id { get; set; }

        public BeerData Data { get; set; }

        public BeerRating()
        {

        }

        public BeerRating(int id, BeerData data)
        {
            Id = id;
            Data = data;
        }
    }
}
