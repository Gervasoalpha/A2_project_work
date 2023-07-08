using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_project_work.Infrastructure.Repositories
{
    public class PicRepository : ARepository<Pic, Guid>, IPicRepository
    {
        public PicRepository(IConfiguration configuration, string classname = "pics") : base(configuration, classname) { }

        public override Task<IEnumerable<Pic>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async override Task<Pic> GetByIdAsync(Guid id)
        {
            string query = $@"
SELECT
    id,
    portnumber,
    buildingnumber,
    status
FROM
    {classname}
WHERE 
    id = @id
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<Pic>(query, new { id });
        }

        public async override Task InsertAsync(Pic entity)
        {
            string query = $@"
INSERT INTO {classname} (id,portnumber,buildingnumber)
VALUES (@id,@port_number,@buildingnumber)
    
";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public override Task UpdateAsync(Pic entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> GetPicIdGivenPortnumberAndRaspberryId(int portnumber, int buildingnumber)
        {
            string query = $@"
SELECT
    id
FROM
    {classname}
WHERE 
    portnumber = @portnumber
AND 
    buildingnumber = @buildingnumber
";
            using var connection = new SqlConnection(_connectionString);

            var pic = await connection.QueryFirstOrDefaultAsync<Guid>(query, new { portnumber, buildingnumber});
            return pic;
        }

        public async Task UpdatePicStatus(bool status,Guid id)
        {

            var query = @$"UPDATE [dbo].[{classname}]
           SET [status] = @status
           WHERE [id] = @id 
";
            using var sql = new SqlConnection(_connectionString);
            await sql.ExecuteAsync(query, new { id, status });
        }
    }
}
