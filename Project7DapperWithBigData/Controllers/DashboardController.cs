using Microsoft.AspNetCore.Mvc;
using Project7DapperWithBigData.Repositories.PlateRepositories;

namespace Project7DapperWithBigData.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IPlateRepository _plateRepository;

        public DashboardController(IPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Basic Bar Chart
            var fuelDistribution = await _plateRepository.GetFuelDistributionAsync();
            ViewBag.FuelDistribution = fuelDistribution;

            // Gradient Bar Chart
            var brandData = await _plateRepository.GetVehicleCountByBrandAsync();
            ViewBag.BrandDistribution = brandData;

            //Stacked Bar Chart
            var fuelDistributionGroupByYear = await _plateRepository.GetFuelDistributionByYearAsync();
            ViewBag.FuelDistribution = fuelDistributionGroupByYear;

            //Basic Line Chart
            var newVehicleRegistrations = await _plateRepository.GetNewVehicleRegistrationsByMonthAsync();
            ViewBag.NewVehicleRegistrations = newVehicleRegistrations;

            //Gradient Line Chart
            var monthlyVehicleRegistrationsByAllBrands = await _plateRepository.GetMonthlyVehicleRegistrationsByAllBrandsAsync();
            ViewBag.MonthlyVehicleRegistrationsByAllBrands = monthlyVehicleRegistrationsByAllBrands;

            //Dual Line Chart
            var fuelData = await _plateRepository.GetAnnualFuelTypeVehicleCountAsync();
            ViewBag.AnnualFuelTypeData = fuelData;

            //Basic Area Chart
            var annualVehicleCountData = await _plateRepository.GetAnnualVehicleCountAsync();
            ViewBag.AnnualVehicleCountData = annualVehicleCountData;

            //Gradient Area Chart
            var annualTopFuelTypeData = await _plateRepository.GetAnnualTopFuelTypeAsync();
            ViewBag.AnnualTopFuelTypeData = annualTopFuelTypeData;

            //Radar Chart
            var categoryData = await _plateRepository.GetCategoryWiseVehicleCountsAsync();
            ViewBag.CategoryWiseData = categoryData;

            //Pie Chart
            var colorData = await _plateRepository.GetColorDistributionAsync();
            ViewBag.ColorDistribution = colorData;

            return View();
        }

        public async Task<IActionResult> Top5Lists()
        {
            var top5Brands = await _plateRepository.GetTop5BrandsAsync();
            ViewBag.Top5Brands = top5Brands;

            // Top 5 Fuel Types
            var top5FuelTypes = await _plateRepository.GetTop5FuelTypesAsync();
            ViewBag.Top5FuelTypes = top5FuelTypes;

            // Top 5 Colors
            var top5Colors = await _plateRepository.GetTop5ColorsAsync();
            ViewBag.Top5Colors = top5Colors;

            // Top 5 Engine Capacities
            var top5EngineCapacities = await _plateRepository.GetTop5EngineCapacitiesAsync();
            ViewBag.Top5EngineCapacities = top5EngineCapacities;

            // Top 5 Case Types
            var top5CaseTypes = await _plateRepository.GetTop5CaseTypesAsync();
            ViewBag.Top5CaseTypes = top5CaseTypes;

            return View();
        }
    }
}
