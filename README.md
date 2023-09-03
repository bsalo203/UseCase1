# Countries API Readme

## Application Description
The Countries API is designed to provide comprehensive data about countries from all around the world. The core of this API revolves around an endpoint that fetches country information, offering capabilities to filter, sort, and limit results based on various criteria. By leveraging a service-driven architecture, the API seamlessly interfaces with the `ICountryService` to handle data retrieval and manipulation, ensuring optimized performance and maintainability. Clients can make intuitive GET requests to extract country details either in bulk or based on specific attributes, making it versatile for diverse application needs.

The underlying architecture is robust, prioritizing data integrity and error handling. If any exception arises during the processing of a request, the API ensures to provide a descriptive error message, guiding the client in making corrections. This results in a reliable and user-friendly experience for developers and applications harnessing the power of country-related data.

## Running the Application Locally
1. Ensure you have the .NET SDK installed on your local machine.
2. Clone the repository: `git clone https://github.com/bsalo203/UseCase1.git`.
3. Navigate to the project directory: `cd UseCase1`.
4. Install required dependencies: `dotnet restore`.
5. Build the project: `dotnet build`.
6. Run the application: `dotnet run`.

## Examples of Using the Endpoint
Given that the main endpoint is a GET request to `api/Countries`, here are some example usages:

1. **Fetch All Countries:** 
   - `GET /api/Countries`
2. **Filter by Country Name:** 
   - `GET /api/Countries?CountryName=Canada`
3. **Filter by Population:** 
   - `GET /api/Countries?PopulationInMillions=100`
4. **Sort Countries by Name in Ascending Order:** 
   - `GET /api/Countries?SortByCountryName=ascend`
5. **Sort Countries by Name in Descending Order:** 
   - `GET /api/Countries?SortByCountryName=descend`
6. **Limit Number of Returned Records:** 
   - `GET /api/Countries?Limit=10`
7. **Filter by Country Name & Population:** 
   - `GET /api/Countries?CountryName=In&PopulationInMillions=10`
8. **Filter by Country Name & Sort:** 
   - `GET /api/Countries?CountryName=India&SortByCountryName=descend`
9. **Sort & Limit Records:** 
   - `GET /api/Countries?SortByCountryName=ascend&Limit=5`
10. **Combine Multiple Filters & Sort:** 
   - `GET /api/Countries?CountryName=USA&PopulationInMillions=3&SortByCountryName=ascend&Limit=5`

Use the provided examples as a starting point and explore further combinations as per your application's requirements.
