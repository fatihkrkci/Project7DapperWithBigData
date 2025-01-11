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

        public async Task<object> GetAnnualFuelTypeVehicleCountAsync()
        {
            string query = @"
            SELECT 
                YEAR(LICENCEDATE) AS Year,
                FUEL,
                COUNT(*) AS VehicleCount
            FROM PLATES
            WHERE FUEL IN ('Benzin', 'Dizel')
            GROUP BY YEAR(LICENCEDATE), FUEL
            ORDER BY Year";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<object> GetAnnualTopFuelTypeAsync()
        {
            var query = @"
            WITH FuelCounts AS (
            SELECT 
            YEAR(LICENCEDATE) AS Year,
            FUEL,
            COUNT(*) AS FuelTypeCount
            FROM PLATES
            GROUP BY YEAR(LICENCEDATE), FUEL
                ),
            RankedFuelCounts AS (
            SELECT 
                Year,
            FUEL,
            FuelTypeCount,
            RANK() OVER (PARTITION BY Year ORDER BY FuelTypeCount DESC) AS Rank
            FROM FuelCounts
            )
            SELECT 
                Year,
                FUEL AS TopFuelType,
                FuelTypeCount
            FROM RankedFuelCounts
            WHERE Rank = 1
            ORDER BY Year;
            ";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<object> GetAnnualVehicleCountAsync()
        {
            var query = @"
            SELECT 
                YEAR(LICENCEDATE) AS Year,
                COUNT(*) AS VehicleCount
            FROM PLATES
            GROUP BY YEAR(LICENCEDATE)
            ORDER BY Year;
        ";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<object> GetCategoryWiseVehicleCountsAsync()
        {
            var query = @"
            SELECT 
                'Fuel' AS Category, FUEL AS SubCategory, COUNT(*) AS VehicleCount
            FROM PLATES
            GROUP BY FUEL

            UNION ALL

            SELECT 
                'ShiftType' AS Category, SHIFTTYPE AS SubCategory, COUNT(*) AS VehicleCount
            FROM PLATES
            GROUP BY SHIFTTYPE

            UNION ALL

            SELECT 
                'CaseType' AS Category, CASETYPE AS SubCategory, COUNT(*) AS VehicleCount
            FROM PLATES
            GROUP BY CASETYPE;
        ";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<object> GetColorDistributionAsync()
        {
            var query = @"
            SELECT 
                COLOR AS Color, 
                COUNT(*) AS VehicleCount, 
                CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER () AS DECIMAL(5, 2)) AS Percentage
            FROM PLATES
            GROUP BY COLOR
            ORDER BY Percentage DESC;
        ";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<List<object>> GetFuelDistributionAsync()
        {
            string query = "SELECT DISTINCT Fuel, COUNT(*) AS VehicleCount FROM PLATES GROUP BY Fuel";
            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync(query);
            return result.ToList();
        }

        public async Task<object> GetFuelDistributionByYearAsync()
        {
            string query = @"
            SELECT Year_, Fuel, COUNT(*) AS VehicleCount
            FROM PLATES
            GROUP BY Year_, Fuel
            ORDER BY Year_, Fuel";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var values = await connection.QueryAsync(query);
            return values;
        }

        public async Task<object> GetMonthlyVehicleRegistrationsByAllBrandsAsync()
        {
            string query = @"
            SELECT Brand, MONTH(LICENCEDATE) AS Month, COUNT(*) AS VehicleCount
            FROM PLATES
            WHERE LICENCEDATE IS NOT NULL
            GROUP BY Brand, MONTH(LICENCEDATE)
            ORDER BY Brand, MONTH(LICENCEDATE)";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var values = await connection.QueryAsync(query);
            return values;
        }

        public async Task<object> GetNewVehicleRegistrationsByMonthAsync()
        {
            string query = @"
            SELECT MONTH(LICENCEDATE) AS Month, COUNT(*) AS VehicleCount
            FROM PLATES
            WHERE LICENCEDATE IS NOT NULL
            GROUP BY MONTH(LICENCEDATE)
            ORDER BY MONTH(LICENCEDATE)";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var values = await connection.QueryAsync(query);
            return values;
        }

        public async Task<List<object>> GetVehicleCountByBrandAsync()
        {
            string query = "SELECT Brand, COUNT(*) as VehicleCount FROM PLATES GROUP BY Brand";
            var connection = _dapperWithBigDataContext.CreateConnection();
            var values = await connection.QueryAsync(query);
            return values.ToList();
        }

        public async Task<List<dynamic>> GetTop5BrandsAsync()
        {
            string query = @"
    SELECT TOP 5 
        BRAND, 
        COUNT(*) AS VehicleCount
    FROM PLATES
    GROUP BY BRAND
    ORDER BY VehicleCount DESC";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<List<dynamic>> GetTop5FuelTypesAsync()
        {
            string query = @"
    SELECT TOP 5 
        FUEL AS FuelType, 
        COUNT(*) AS VehicleCount
    FROM PLATES
    GROUP BY FUEL
    ORDER BY VehicleCount DESC";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<List<dynamic>> GetTop5ColorsAsync()
        {
            string query = @"
    SELECT TOP 5 
        COLOR AS Color, 
        COUNT(*) AS VehicleCount
    FROM PLATES
    GROUP BY COLOR
    ORDER BY VehicleCount DESC";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<List<dynamic>> GetTop5EngineCapacitiesAsync()
        {
            string query = @"
    SELECT TOP 5 
        MOTORVOLUME AS EngineCapacity, 
        COUNT(*) AS VehicleCount
    FROM PLATES
    GROUP BY MOTORVOLUME
    ORDER BY VehicleCount DESC";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }

        public async Task<List<dynamic>> GetTop5CaseTypesAsync()
        {
            string query = @"
    SELECT TOP 5 
        CASETYPE AS CaseType, 
        COUNT(*) AS VehicleCount
    FROM PLATES
    GROUP BY CASETYPE
    ORDER BY VehicleCount DESC";

            var connection = _dapperWithBigDataContext.CreateConnection();
            var result = await connection.QueryAsync<dynamic>(query);
            return result.ToList();
        }
    }
}