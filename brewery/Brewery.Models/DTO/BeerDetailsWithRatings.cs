using System.Text.Json.Serialization;

namespace Brewery.Models.DTO
{
    /// <summary>
    /// Incorporates beer details and user ratings
    /// </summary>
    /// <remarks>JsonPropertyOrder used to put user ratings in the end order during serialization</remarks>
    public class BeerDetailsWithRatings: BeerDetails
    {
        [JsonPropertyOrder(int.MaxValue)]
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
