using System.ComponentModel.DataAnnotations;

namespace Brewery.Models.DTO
{
    public class BeerData
    {
        public string Username { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string? Comments { get; set; }            
    }
}