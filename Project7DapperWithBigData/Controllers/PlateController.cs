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

        public async Task<IActionResult> PlateList(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var allPlates = await _plateRepository.GetAllPlateAsync();
            var pagedPlates = allPlates.ToPagedList(pageNumber, pageSize);

            return View(pagedPlates);
        }
    }
}
