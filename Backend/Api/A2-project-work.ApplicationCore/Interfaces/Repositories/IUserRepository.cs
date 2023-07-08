using A2_project_work.Domain.Entities;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task InsertNoGuid(NoGuidUser rasp);
        Task InsertAdmin(AdminUser admin);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByUsernameAndPassword(string username,string password);
        Task<bool> Isadmin(string username,string password);
    }
}
