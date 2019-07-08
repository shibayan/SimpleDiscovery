using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionApp
{
    public class BuchizoService
    {
        public BuchizoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public Task<string> GetAsync(string path)
        {
            return _httpClient.GetStringAsync(path);
        }
    }
}
