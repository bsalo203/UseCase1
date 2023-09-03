using Microsoft.AspNetCore.Mvc;
using UseCase1.Models;
using UseCase1.Services;
using UseCase1.Services.Interfaces;

namespace UseCase1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Countries : ControllerBase
    {
        readonly ICountryService countriesService;
        public Countries(ICountryService _countriesService)
        {
            countriesService = _countriesService;
        }

        [HttpGet]
        public List<OutputModel> GetAllCountries([FromQuery] InputModel input)
        {
            return countriesService.GetAllCountries().Result;
        }
    }
}
