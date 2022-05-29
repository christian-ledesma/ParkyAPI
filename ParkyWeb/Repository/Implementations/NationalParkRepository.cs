using ParkyWeb.Models;
using ParkyWeb.Repository.Interfaces;
using System.Net.Http;

namespace ParkyWeb.Repository.Implementations
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _client;
        public NationalParkRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
