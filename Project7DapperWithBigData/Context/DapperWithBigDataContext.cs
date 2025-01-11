using Microsoft.Data.SqlClient;
using System.Data;

namespace Project7DapperWithBigData.Context
{
    public class DapperWithBigDataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperWithBigDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connectionkey");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
