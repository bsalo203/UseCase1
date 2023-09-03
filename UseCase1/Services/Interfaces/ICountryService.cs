using UseCase1.Models;

namespace UseCase1.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<OutputModel>> FilterByCountryName(string countryName);
        Task<List<OutputModel>> FilterByPopulation(int? population);
        Task<List<OutputModel>> GetAllCountries();
    }
}
