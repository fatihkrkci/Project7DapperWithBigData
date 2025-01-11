using Microsoft.AspNetCore.Mvc;
using Project7DapperWithBigData.Repositories.PlateRepositories;
using X.PagedList.Extensions;

namespace Project7DapperWithBigData.Controllers
{
    public class PlateController : Controller
    {
        private readonly IPlateRepository _plateRepository;

        public PlateController(IPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
        }

        public async Task<IActionResult> PlateList(int? page, string searchTerm)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var allPlates = await _plateRepository.GetAllPlateAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                allPlates = allPlates
                    .Where(p => p.Plate.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                (p.Title != null && p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                (p.Brand != null && p.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            var pagedPlates = allPlates.ToPagedList(pageNumber, pageSize);

            ViewBag.SearchTerm = searchTerm; // Arama terimini View'e göndermek için
            return View(pagedPlates);
        }

    }
}
