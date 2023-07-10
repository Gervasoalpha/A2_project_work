using A2_project_work.Domain.Entities;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface IRaspberryRepository : IRepository<Raspberry,Guid>
    {
        Task InsertNoGuid(NoGuidRasp rasp);

        
    }
}
