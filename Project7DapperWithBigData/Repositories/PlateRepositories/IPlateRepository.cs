using Project7DapperWithBigData.Dtos.PlateDtos;

namespace Project7DapperWithBigData.Repositories.PlateRepositories
{
    public interface IPlateRepository
    {
        Task<List<ResultPlateDto>> GetAllPlateAsync();
        Task<List<object>> GetFuelDistributionAsync();
        Task<List<object>> GetVehicleCountByBrandAsync();
        Task<object> GetFuelDistributionByYearAsync();
        Task<object> GetNewVehicleRegistrationsByMonthAsync();
        Task<object> GetMonthlyVehicleRegistrationsByAllBrandsAsync();
        Task<object> GetAnnualFuelTypeVehicleCountAsync();
        Task<object> GetAnnualVehicleCountAsync();
        Task<object> GetAnnualTopFuelTypeAsync();
        Task<object> GetCategoryWiseVehicleCountsAsync();
        Task<object> GetColorDistributionAsync();
        Task<List<dynamic>> GetTop5BrandsAsync();
        Task<List<dynamic>> GetTop5FuelTypesAsync();
        Task<List<dynamic>> GetTop5ColorsAsync();
        Task<List<dynamic>> GetTop5EngineCapacitiesAsync();
        Task<List<dynamic>> GetTop5CaseTypesAsync();
    }
}
