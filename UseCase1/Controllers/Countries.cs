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
        public IActionResult GetAllCountries([FromQuery] InputModel input)
        {
            try
            {
                countriesService.GetAllCountries();
                if (!string.IsNullOrEmpty(input.CountryName))
                    countriesService.FilterByCountryName(input.CountryName);
                if (input.PopulationInMillions != null)
                    countriesService.FilterByPopulation((int)input.PopulationInMillions);
                if (!string.IsNullOrEmpty(input.SortByCountryName))
                    countriesService.SortByCountryName(input.SortByCountryName ?? string.Empty);
                if (input.Limit != null)
                    countriesService.LimitRecords((int)input.Limit);
                return Ok(countriesService.ListOfCountries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
