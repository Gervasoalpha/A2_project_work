using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

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
            
            return await connection.QueryFirstAsync<long>(query, new {pic_id});
        }

        public async Task<string> GetUnlockCode(string authcode)
        {

            const string query = @"
SELECT
    unlockcode
FROM
    logs
WHERE authcode = @authcode
";
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var res = await connection.QueryFirstAsync<string>(query, new { authcode });
                return res;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
          
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

        public async Task<IEnumerable<GetLog>> PrettyGet()
        {
            const string query = @"
SELECT l.id,authcode, unlockcode, [opened?],u.name,p.portnumber 
FROM logs l
LEFT JOIN Users u ON l.user_id = u.id LEFT JOIN Pics p ON p.id = l.pic_id


";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<GetLog>(query);
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
        public async Task UpdateUser(UsernameUserId us)
        {

            var query = @$"UPDATE [dbo].[{classname}]
           SET [user_id] = @userid
           WHERE [authcode] = @authcode
";
            using var sql = new SqlConnection(_connectionString);
            await sql.ExecuteAsync(query, new { us.authcode, us.userid});
        }


    }
}
