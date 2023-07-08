using A2_project_work.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface IRaspberryRepository : IRepository<Raspberry,Guid>
    {
        Task InsertNoGuid(NoGuidRasp rasp);
    }
}
