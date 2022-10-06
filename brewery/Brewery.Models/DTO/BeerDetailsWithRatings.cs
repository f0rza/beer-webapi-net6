using System.Text.Json.Serialization;

namespace Brewery.Models.DTO
{
    public class BeerDetailsWithRatings: BeerDetails
    {
        [JsonPropertyOrder(100)]
        public IList<BeerData> UserRatings { get; set; }

        public BeerDetailsWithRatings()
        {

        }

        public BeerDetailsWithRatings(BeerDetails beerDetails, IList<BeerData> userRatings)
        {
            Id = beerDetails.Id;
            Name = beerDetails.Name;
            Description = beerDetails.Description;
            UserRatings = userRatings;
        }
    }
}
