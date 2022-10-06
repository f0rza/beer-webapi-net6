using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Brewery.Models;

namespace Brewery.Repositories
{
    /// <summary>
    /// Repository works with local file to save and get user ratings
    /// </summary>
    public class LocalFileRepository : ILocalFileRepository
    {
        private readonly JsonSerializerOptions _options =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        private readonly IConfiguration _configuration;
        private readonly string _fileName;

        public LocalFileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileName = _configuration.GetConnectionString("LocalFile");
        }

        /// <summary>
        /// Adds new rating to file
        /// </summary>
        /// <param name="beerRating"></param>
        /// <returns></returns>
        public async Task AddRating(BeerRating beerRating)
        {
            var list = await GetAllRatings();

            await using var fileStream = new FileStream(_fileName, FileMode.Create);
            await JsonSerializer.SerializeAsync(fileStream, list.Append(beerRating), _options);
        }

        /// <summary>
        /// Reads all ratings from file
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BeerRating>> GetAllRatings()
        {
            await using var fileStream = new FileStream(_fileName, FileMode.OpenOrCreate);

            if (fileStream.Length == 0)
                return new List<BeerRating>();

            return await JsonSerializer.DeserializeAsync<IEnumerable<BeerRating>>(fileStream, _options);
        }
    }
}
