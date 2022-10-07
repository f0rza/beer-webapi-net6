using System.ComponentModel.DataAnnotations;

namespace Brewery.Models.DTO
{
    /// <summary>
    /// Relfects data collected from user
    /// </summary>
    /// <remarks>DataAnnotations attributes used to handle model validation</remarks>
    public class BeerData
    {
        public string Username { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string? Comments { get; set; }            
    }
}