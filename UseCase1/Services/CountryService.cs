using System.Reflection.Metadata.Ecma335;
using UseCase1.Clients;
using UseCase1.Clients.Interfaces;
using UseCase1.Controllers;
using UseCase1.Models;
using UseCase1.Services.Interfaces;

namespace UseCase1.Services
{
    public class CountryService : ICountryService
    {
        readonly ICountryClient countriesClient;
        private List<OutputModel> listOfCountries = new List<OutputModel>();
        public List<OutputModel> ListOfCountries
        {
            get { return listOfCountries; }
        }
        public CountryService(ICountryClient _countriesClient)
        {
            countriesClient = _countriesClient;
        }

        public void FilterByCountryName(string countryName)
        {
            listOfCountries = listOfCountries.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(countryName, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void FilterByPopulation(int populationInMillions)
        {
            int realPopulation = (populationInMillions == 0 ? 1 : populationInMillions) * 1000000;
            listOfCountries = listOfCountries.Where(c => c.Population < realPopulation).ToList();
        }

        public void GetAllCountries()
        {
            List<Country> countryList = countriesClient.GetAllCountriesAsync().Result ?? new List<Country>();
            listOfCountries = countryList.Select(x => x.ConvertToOutput()).ToList();
        }

        public void LimitRecords(int limit)
        {
            listOfCountries = listOfCountries.Take(limit).ToList();
        }

        public void SortByCountryName(string sortCountryName)
        {
            if (sortCountryName.ToLower() == "ascend")
            {
                listOfCountries = listOfCountries.OrderBy(c => c.Name).ToList();
            }
            else if (sortCountryName.ToLower() == "descend")
            {
                listOfCountries = listOfCountries.OrderByDescending(c => c.Name).ToList();
            }
            else
            {
                throw new ArgumentException("Invalid sort order. Use 'ascend' or 'descend'.");
            }
        }
    }
}
