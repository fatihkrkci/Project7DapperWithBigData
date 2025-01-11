using Dapper;
using Microsoft.Data.SqlClient;
using Project7DapperWithBigData.Context;
using Project7DapperWithBigData.Dtos.PlateDtos;

namespace Project7DapperWithBigData.Repositories.PlateRepositories
{
    public class PlateRepository : IPlateRepository
    {
        private readonly DapperWithBigDataContext _dapperWithBigDataContext;

        public PlateRepository(DapperWithBigDataContext dapperWithBigDataContext)
        {
            _dapperWithBigDataContext = dapperWithBigDataContext;
        }

        public async Task<List<ResultPlateDto>> GetAllPlateAsync()
        {
            string query = "Select * From PLATES";
            var connection = _dapperWithBigDataContext.CreateConnection();
            var values = await connection.QueryAsync<ResultPlateDto>(query);
            return values.ToList();
        }
    }
}
