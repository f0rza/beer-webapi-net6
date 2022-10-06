using Brewery.Exceptions;
using Brewery.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Brewery.Repositories
{
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
        /// Gets beer details from Punk API
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
    }
}