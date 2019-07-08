using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MvcApp.Services;

namespace MvcApp.Controllers
{
    public class BuchizoController : Controller
    {
        public BuchizoController(BuchizoService buchizoService)
        {
            _buchizoService = buchizoService;
        }

        private readonly BuchizoService _buchizoService;

        public async Task<IActionResult> Index()
        {
            var response = await _buchizoService.GetAsync("/");

            return Content(response);
        }
    }
}