using UseCase1.Clients;
using UseCase1.Clients.Interfaces;
using UseCase1.Models;
using UseCase1.Services.Interfaces;

namespace UseCase1.Services
{
    public class CountryService : ICountryService
    {
        ICountryClient countriesClient;
        public CountryService(ICountryClient _countriesClient)
        {
            countriesClient = _countriesClient;
        }
        public async Task<List<OutputModel>> GetAllCountries()
        {
            List<Country> countryList = await countriesClient.GetAllCountriesAsync();

            List<OutputModel> outputModel = countryList.Select(x => x.ConvertToOutput()).ToList();

            return outputModel;
        }
    }
}
