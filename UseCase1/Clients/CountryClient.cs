using System.Text.Json;
using UseCase1.Clients.Interfaces;
using UseCase1.Models;

namespace UseCase1.Clients
{
    public class CountryClient : ICountryClient
    {
        private readonly HttpClient _httpClient;

        public CountryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            using (var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all"))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var countries = JsonSerializer.Deserialize<List<Country>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return countries;
            }
        }
    }
}
