using UseCase1.Models;

namespace UseCase1.Clients.Interfaces
{
    public interface ICountryClient
    {
        Task<List<Country>?> GetAllCountriesAsync();
    }
}
