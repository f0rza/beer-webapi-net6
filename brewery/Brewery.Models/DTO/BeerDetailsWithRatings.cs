namespace Brewery.Models.DTO
{
    public class BeerDetailsWithRatings: BeerDetails
    {
        public IList<BeerData> UserRatings { get; set; }
    }
}
