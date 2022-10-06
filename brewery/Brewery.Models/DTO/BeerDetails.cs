using System.Text.Json.Serialization;

namespace Brewery.Models.DTO
{
    public class BeerDetails
    {
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [JsonPropertyOrder(2)]
        public string Name { get; set; }
        [JsonPropertyOrder(3)]
        public string Description { get; set; }
    }
}
