namespace UseCase1.Models
{
    public class InputModel
    {
        public string? CountryName { get; set; }
        public int? PopulationInMillions { get; set; }
        public string? SortByCountryName { get; set; }
        public int? Limit { get; set; }
    }
}
