using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers
{
    public class BuchizoController : Controller
    {
        public BuchizoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("Buchizo");

            var response = await httpClient.GetStringAsync("/");

            return Content(response);
        }
    }
}