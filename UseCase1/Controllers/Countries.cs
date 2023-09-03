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
                List<OutputModel> countries = countriesService.GetAllCountries().Result;
                if (!string.IsNullOrEmpty(input.CountryName))
                    return Ok(countriesService.FilterByCountryName(input.CountryName).Result);
                else if (input.PopulationInMillions != null)
                    return Ok(countriesService.FilterByPopulation(input.PopulationInMillions).Result);
                else if (!string.IsNullOrEmpty(input.SortCountryName))
                    return Ok(countriesService.SortByCountryName(input.SortCountryName ?? string.Empty).Result);
                else
                    return Ok(countries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
}
    }
}
