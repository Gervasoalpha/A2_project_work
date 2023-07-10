using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Dapper.SqlMapper;

namespace A2_project_work.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db")!;
        }

        public async Task DeleteAll()
        {
            const string query = @"DELETE FROM users";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query);
        }

        public async Task DeleteAsync(Guid id)
        {
            const string query = @"DELETE FROM users WHERE id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new { id });
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            const string query = @"
SELECT
    id,
    name,
    surname,
    username,
    password,
    email,
    [admin?] as admin
FROM
    users
";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<User>(query);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            const string query = @"
SELECT
    id,
    name,
    surname,
    username,
    password,
    email,
    [admin?] as admin
FROM
    users
WHERE 
    id = @id
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
        }

        public async Task<User> GetUserByName(string name)
        {
            const string query = @"
SELECT
    id,
    name,
    surname,
    username,
    password,
    email,
    [admin?] as admin
FROM
    users
WHERE 
    name = @name
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<User>(query, new { name });
        }


        public async Task<User> GetUserByUsername(string username)
        {
            const string query = @"
SELECT
    id,
    name,
    surname,
    username,
    password,
    email,
    [admin?] as admin
FROM
    users
WHERE 
    username = @username
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<User>(query, new { username });
        }


        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            const string query = @"
SELECT
    id,
    name,
    surname,
    username,
    password,
    email,
    [admin?] as admin
FROM
    users
WHERE 
    username = @username
AND
    password = @password
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<User>(query, new { username, password });
        }


        public async Task InsertAdmin(AdminUser entity)
        {
            string query = "";
            query = @"
INSERT INTO users (id,name,surname,username,password, [admin?], email)
VALUES (@id,@name,@surname,@username,@password, @admin, @email)";



            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async Task InsertAsync(User entity)
        {
            string query = "";
            query = @"
INSERT INTO users (id,name,surname,username,password,email)
VALUES (@id,@name,@surname,@username,@password,@email)";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async Task UpdateAsync(User entity)
        {
            const string query = @"
UPDATE users 
SET
    id = @id,
    name = @name,
    surname = @surname,
    username= @username,
    password = @password,
    email = @email,
    [admin?] = @admin
";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }
        public async Task<bool> Isadmin(string username, string password)
        {

            const string query = @"
SELECT
    [admin?] as admin
FROM
    users
WHERE 
    username = @username
AND
    password = @password
";
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<bool>(query, new { username, password });

        }

        public async Task<long> Count()
        {
            const string query = "SELECT COUNT(*) FROM users";
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<long>(query);
        }

        public async Task InsertNoGuid(NoGuidUser entity)
        {
            string query = @"
INSERT INTO users (id,name,surname,username,password,email)
VALUES (NEWID(),@name,@surname,@username,@password,@email)";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, entity);
        }

        public async Task GiveAdminPerms(UsernameAndEmailUser user)
        {
            string query = @"
UPDATE users
SET [admin?] = 1
WHERE username = @username AND email = @email";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, user);
        }

        
    }
}
