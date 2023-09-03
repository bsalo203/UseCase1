using UseCase1.Models;

namespace UseCase1.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<OutputModel>> FilterByCountryName(string countryName);
        Task<List<OutputModel>> FilterByPopulation(int? populationInMillions);
        Task<List<OutputModel>> GetAllCountries();
        Task<List<OutputModel>> LimitRecords(int limit);
        Task<List<OutputModel>> SortByCountryName(string sortCountryName);
    }
}
