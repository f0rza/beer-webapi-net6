using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Brewery.Models;
using System.Xml;

namespace Brewery.Repositories
{
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
               
        public async Task AddRating(BeerRating beerRating)
        {
            await using var fileStream = new FileStream(_fileName, FileMode.Append);
            await JsonSerializer.SerializeAsync(fileStream, beerRating, _options);
        }
    }
}
