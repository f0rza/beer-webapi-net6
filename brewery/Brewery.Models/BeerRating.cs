using Brewery.Models.DTO;

namespace Brewery.Models
{
    /// <summary>
    /// Class implements record for JSON storage
    /// </summary>
    public class BeerRating
    {
        public int Id { get; set; }

        public BeerUserRating Data { get; set; }

        public BeerRating()
        {

        }

        public BeerRating(int id, BeerUserRating data)
        {
            Id = id;
            Data = data;
        }
    }
}
