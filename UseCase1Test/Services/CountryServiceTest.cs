using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase1.Clients.Interfaces;
using UseCase1.Models;
using UseCase1.Services;
using UseCase1.Services.Interfaces;

namespace UseCase1Test.Services
{
    [TestClass]
    public class CountryServiceTest
    {
        private Mock<ICountryClient> mockCountriesClient;  // Assuming the interface is named ICountriesClient
        private CountryService countryService;  // Replace with the name of the class containing GetAllCountries

        private List<Country> LoadTestCountriesFromJson()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "testCountries.json");
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Country>>(json);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockCountriesClient = new Mock<ICountryClient>();
            countryService = new CountryService(mockCountriesClient.Object); // Assuming constructor injection
        }

        #region GetAllCountries Tests
        [TestMethod]
        public async Task GetAllCountries_PopulatesListOfCountriesCorrectly()
        {
            // Arrange
            var mockCountries = LoadTestCountriesFromJson();

            mockCountriesClient
                .Setup(client => client.GetAllCountriesAsync())
                .ReturnsAsync(mockCountries);

            // Act
            countryService.GetAllCountries();

            // Assert
            Assert.AreEqual(countryService.ListOfCountries.Count, mockCountries.Count);
        }

        [TestMethod]
        public void GetAllCountries_WhenClientReturnsNull_InitializesEmptyList()
        {
            // Arrange
            mockCountriesClient.Setup(client => client.GetAllCountriesAsync()).ReturnsAsync((List<Country>)null);
            var service = new CountryService(mockCountriesClient.Object);

            // Act
            service.GetAllCountries();

            // Assert
            Assert.AreEqual(0, service.ListOfCountries.Count);
        }
        #endregion

        #region FilterByCountryName Tests
        [TestMethod]
        public void FilterByCountryName_WithMatchingCountryName_FiltersCorrectly()
        {
            var mockCountries = LoadTestCountriesFromJson();

            mockCountriesClient
                .Setup(client => client.GetAllCountriesAsync())
                .ReturnsAsync(mockCountries);

            // Act
            countryService.GetAllCountries();
            countryService.FilterByCountryName("an");

            // Assert
            Assert.AreEqual(countryService.ListOfCountries.Count, mockCountries.Where(c => !string.IsNullOrEmpty(c.Name.Common) && c.Name.Official.Contains("an", System.StringComparison.OrdinalIgnoreCase)).ToList().Count);
        }

        [TestMethod]
        public void FilterByCountryName_WithNoMatchingCountryName_ReturnsEmptyList()
        {
            var mockCountries = LoadTestCountriesFromJson();

            mockCountriesClient
                .Setup(client => client.GetAllCountriesAsync())
                .ReturnsAsync(mockCountries);

            // Act
            countryService.GetAllCountries();
            countryService.FilterByCountryName("Australia");

            // Assert
            Assert.IsFalse(countryService.ListOfCountries.Any());
        }

        [TestMethod]
        public void FilterByCountryName_WhenListContainsNullNames_ExcludesNullNames()
        {
            // Arrange
            var mockCountries = LoadTestCountriesFromJson();

            mockCountriesClient
                .Setup(client => client.GetAllCountriesAsync())
                .ReturnsAsync(mockCountries);

            // Act
            countryService.GetAllCountries();
            countryService.FilterByCountryName("Venezuela");

            // Assert
            Assert.AreEqual(1, countryService.ListOfCountries.Count);
            Assert.AreEqual("Bolivarian Republic of Venezuela", countryService.ListOfCountries.First().Name);
        }
        #endregion

        #region FilterByPopulation Tests
        [TestMethod]
        public void FilterByPopulation_WithValidPopulation_FiltersCorrectly()
        {
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "CountryA" }, Population = 500000 },
                new Country { Name = new Name{Official="CountryB" }, Population = 1500000 },
                new Country { Name = new Name{Official= "CountryC" }, Population = 3000000 }
            };

            mockCountriesClient
                .Setup(client => client.GetAllCountriesAsync())
                .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.FilterByPopulation(2); // 2 million, or 2000000

            // Assert
            Assert.AreEqual(2, countryService.ListOfCountries.Count);
        }

        [TestMethod]
        public void FilterByPopulation_WithZeroPopulation_FiltersForOneMillion()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official = "CountryA" }, Population = 500000 },
                new Country { Name = new Name{Official= "CountryB" }, Population = 1500000 },
            };

            mockCountriesClient
               .Setup(client => client.GetAllCountriesAsync())
               .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.FilterByPopulation(1);

            // Assert
            Assert.AreEqual(1, countryService.ListOfCountries.Count);
            Assert.AreEqual("CountryA", countryService.ListOfCountries.First().Name);
        }
        #endregion

        #region SortByCountryName Tests

        [TestMethod]
        public void SortByCountryName_Ascending_SortsCorrectly()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.SortByCountryName("ascend");

            // Assert
            Assert.AreEqual("Canada", countryService.ListOfCountries[0].Name);
            Assert.AreEqual("India", countryService.ListOfCountries[1].Name);
            Assert.AreEqual("USA", countryService.ListOfCountries[2].Name);
        }

        [TestMethod]
        public void SortByCountryName_Descending_SortsCorrectly()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.SortByCountryName("descend");

            // Assert
            Assert.AreEqual("USA", countryService.ListOfCountries[0].Name);
            Assert.AreEqual("India", countryService.ListOfCountries[1].Name);
            Assert.AreEqual("Canada", countryService.ListOfCountries[2].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortByCountryName_InvalidInput_ThrowsException()
        {
            // Arrange
            var listOfCountries = new List<Country>();
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);


            // Act
            countryService.GetAllCountries();
            countryService.SortByCountryName("invalid");
        }

        #endregion

        #region LimitRecords Tests

        [TestMethod]
        public void LimitRecords_ValidLimit_LimitsCorrectly()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.LimitRecords(2);

            // Assert
            Assert.AreEqual(2, countryService.ListOfCountries.Count);
        }

        [TestMethod]
        public void LimitRecords_LimitExceedsList_ReturnsEntireList()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.LimitRecords(5);

            // Assert
            Assert.AreEqual(3, countryService.ListOfCountries.Count);
        }

        [TestMethod]
        public void LimitRecords_ZeroLimit_ReturnsEmptyList()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.LimitRecords(0);

            // Assert
            Assert.AreEqual(0, countryService.ListOfCountries.Count);
        }

        [TestMethod]
        public void LimitRecords_NegativeLimit_ReturnsEmptyList()
        {
            // Arrange
            var listOfCountries = new List<Country>
            {
                new Country { Name = new Name{Official= "USA" } },
                new Country { Name = new Name{Official= "India" } },
                new Country { Name = new Name { Official = "Canada" } }
            };
            mockCountriesClient
                 .Setup(client => client.GetAllCountriesAsync())
                 .ReturnsAsync(listOfCountries);

            // Act
            countryService.GetAllCountries();
            countryService.LimitRecords(-3);

            // Assert
            Assert.AreEqual(0, countryService.ListOfCountries.Count);
        }


        #endregion
    }

}

