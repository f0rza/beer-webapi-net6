using Brewery.Exceptions;
using Brewery.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Brewery.Repositories
{
    /// <summary>
    /// Service working with Punk API
    /// </summary>
    public class BeerStorageClient : IBeerStorageClient
    {
        private readonly IConfiguration _configuration;

        public BeerStorageClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets beer details
        /// </summary>
        /// <param name="id">beer Id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        /// <exception cref="BeerNotFoundException"></exception>
        public async Task<BeerDetails> GetBeerDetails(int id, CancellationToken cancellationToken)
        {
            using var _client = GetClient();
            var request = new RestRequest($"beers/{id}");
            var response = await _client.GetAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<IEnumerable<BeerDetails>>(response.Content).First();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new BeerNotFoundException(response.Content);

            return null;
        }

        /// <summary>
        /// Returns all beers matching the supplied name (this will match partial strings as well so e.g punk will return Punk IPA), if you need to add spaces just add an underscore (_).
        /// </summary>
        /// <param name="name">search value</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<BeerDetails>> GetList(string name, CancellationToken cancellationToken)
        {
            using var _client = GetClient();
            var request = new RestRequest($"beers?beer_name={name}");

            return await _client.GetAsync<IEnumerable<BeerDetails>>(request, cancellationToken);
        }

        /// <summary>
        /// Initializes new RestClient
        /// </summary>
        /// <returns></returns>
        private RestClient GetClient() => new RestClient(_configuration["ApiRoot"]);
    }
}