using UseCase1.Models;

namespace UseCase1.Services.Interfaces
{
    public interface ICountryService
    {
        List<OutputModel> ListOfCountries { get; }
        void FilterByCountryName(string countryName);
        void FilterByPopulation(int populationInMillions);
        void GetAllCountries();
        void LimitRecords(int limit);
        void SortByCountryName(string sortCountryName);
    }
}
