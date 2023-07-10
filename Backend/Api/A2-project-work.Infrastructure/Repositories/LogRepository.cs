using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace A2_project_work.Infrastructure.Repositories
{
    public class LogRepository : ARepository<Log, long>, ILogRepository
    {
        public LogRepository(IConfiguration configuration) : base(configuration, "logs") { }


        public async override Task<IEnumerable<Log>> GetAllAsync()
        {
            const string query = @"
SELECT
    id,
    authcode,
    unlockcode,
    [opened?] as opened,
    user_id,
    pic_id
FROM
    logs
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Log>(query);
        }

        public override Task<Log> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetLastPicLogId(Guid pic_id)
        {
            const string query = @"
SELECT
    id
FROM
    logs
WHERE [pic_id] = @pic_id
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefault(query, new {pic_id});
        }

        public async Task<string> GetUnlockCode(string auhtcode)
        {

            const string query = @"
SELECT
    unlockcode
FROM
    logs
WHERE [authcode] = @authcode
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefault(query, new {auhtcode});
        }

        public async override Task InsertAsync(Log entity)
        {
            string query = $@"
INSERT INTO {classname} (id,authcode,unlockcode,opened?,user_id,pic_id)
VALUES (id,authcode,unlockcode,opened,user_id,pic_id)
    
";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async Task InsertAuthcodeUnlockcodeAndPicGuid(string authcode,string unlockcode, Guid pic_id)
        {
            var query = @$"INSERT INTO [dbo].[{classname}]
           ([authcode],
            [pic_id],
            [unlockcode]
            )
     VALUES
           (@authcode,
            @pic_id,
            @unlockcode)";
            using var sql = new SqlConnection(_connectionString);
            await sql.ExecuteAsync(query,new {authcode,pic_id,unlockcode});
        }

        public override Task UpdateAsync(Log entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePortState(long logid, bool state)
        {
            
            var query = @$"UPDATE [dbo].[{classname}]
           SET [opened?] = @state
           WHERE [logid] = @logid
";
            using var sql = new SqlConnection(_connectionString);
            await sql.ExecuteAsync(query, new {state,logid });
        }

        
    }
}
