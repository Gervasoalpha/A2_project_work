using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace A2_project_work.ApplicationCore.Abstracts.Repositories
{
    public abstract class ARepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey>
    {
        public ARepository(IConfiguration configuration,string classname )
        {
            this._connectionString = configuration.GetConnectionString("db")!;
            this.classname = classname;

        }

        public string? classname { get; set; }
        public string? _connectionString { get; set; }
        public async Task<long> Count()
        {

            string query = $"SELECT COUNT(*) FROM {classname}";
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<long>(query);
        }
        public async Task DeleteAll()
        {

            string query = @$"DELETE FROM {this.classname}";
            using var connection = new SqlConnection(this._connectionString);
            await connection.ExecuteAsync(query);
        }
        public async Task DeleteAsync(TPrimaryKey id)
        {
            string query = $"DELETE FROM {classname} WHERE id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new { id });
        }
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> GetByIdAsync(TPrimaryKey id);
        public abstract Task InsertAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);
    }
}
