using ParkyWeb.Models;
using ParkyWeb.Repository.Interfaces;
using System.Net.Http;

namespace ParkyWeb.Repository.Implementations
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        private readonly IHttpClientFactory _client;
        public TrailRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
