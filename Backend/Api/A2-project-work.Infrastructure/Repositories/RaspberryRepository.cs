using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace A2_project_work.Infrastructure.Repositories
{
    public class RaspberryRepository : ARepository<Raspberry, Guid>,IRaspberryRepository
    {
        public RaspberryRepository(IConfiguration configuration, string classname="raspberries") : base(configuration, classname){}

        public async override Task<IEnumerable<Raspberry>> GetAllAsync()
        {
            string query = @$"
SELECT
    id,
    buildingnumber
FROM
    {classname}
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Raspberry>(query);
        }

        public async override Task<Raspberry> GetByIdAsync(Guid id)
        {
            string query = @$"
SELECT
    id,
    buildingnumber
FROM
    {classname}
WHERE 
 id=@id
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Raspberry>(query,id);
        }

        public async override Task InsertAsync(Raspberry entity)
        {
            string query = $@"
INSERT INTO {classname} (id,buildingnumber)
VALUES (@id,@buildingnumber)
";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async Task InsertNoGuid(NoGuidRasp entity)
        {

            string query = $@"
INSERT INTO {classname} (id,buildingnumber)
VALUES (NEWID(),@buildingnumber)
";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async override Task UpdateAsync(Raspberry entity)
        {
            throw new NotImplementedException();
        }
    }
}
