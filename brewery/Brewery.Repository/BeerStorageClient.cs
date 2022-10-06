using Brewery.Exceptions;
using Brewery.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Brewery.Repositories
{
    /// <summary>
    /// Service working with Punk API
    /// </summary>
    public class BeerStorageClient : IBeerStorageClient
    {
        private readonly ILogger<BeerStorageClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly RestClient _client;

        public BeerStorageClient(ILogger<BeerStorageClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new RestClient(_configuration["ApiRoot"]);
        }

        /// <summary>
        /// Gets beer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BeerNotFoundException"></exception>
        public async Task<BeerDetails> GetBeerDetails(int id)
        {
            var request = new RestRequest($"beers/{id}");
            var response = await _client.GetAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<IEnumerable<BeerDetails>>(response.Content).First();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new BeerNotFoundException(response.Content);

            return null;
        }

        public async Task<IEnumerable<BeerDetails>> GetList(string name)
        {
            var request = new RestRequest($"beers?beer_name={name}");
            
            return await _client.GetAsync<IEnumerable<BeerDetails>>(request);
        }
    }
}