using Project7DapperWithBigData.Dtos.PlateDtos;

namespace Project7DapperWithBigData.Repositories.PlateRepositories
{
    public interface IPlateRepository
    {
        Task<List<ResultPlateDto>> GetAllPlateAsync();
    }
}
