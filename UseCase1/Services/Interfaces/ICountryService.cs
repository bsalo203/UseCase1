using UseCase1.Models;

namespace UseCase1.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<OutputModel>> GetAllCountries();
    }
}
