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
    }
}
