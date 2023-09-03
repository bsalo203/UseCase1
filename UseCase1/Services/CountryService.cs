﻿using UseCase1.Clients;
using UseCase1.Clients.Interfaces;
using UseCase1.Models;
using UseCase1.Services.Interfaces;

namespace UseCase1.Services
{
    public class CountryService : ICountryService
    {
        readonly ICountryClient countriesClient;
        public CountryService(ICountryClient _countriesClient)
        {
            countriesClient = _countriesClient;
        }

        public async Task<List<OutputModel>> FilterByCountryName(string countryName)
        {
            List<OutputModel> _countries = await GetAllCountries();
            return _countries.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(countryName, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<OutputModel>> FilterByPopulation(int? populationInMillions)
        {
            List<OutputModel> _countries = await GetAllCountries();
            int realPopulation = (populationInMillions == 0 ? 1 : populationInMillions ?? 1) * 1000000;
            return _countries.Where(c => c.Population < realPopulation).ToList();
        }

        public async Task<List<OutputModel>> GetAllCountries()
        {
            List<Country> countryList = await countriesClient.GetAllCountriesAsync() ?? new List<Country>();

            List<OutputModel> outputModel = countryList.Select(x => x.ConvertToOutput()).ToList();

            return outputModel;
        }
    }
}
