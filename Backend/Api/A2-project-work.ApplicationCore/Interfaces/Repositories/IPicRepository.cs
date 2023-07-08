using A2_project_work.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface IPicRepository : IRepository<Pic,Guid>
    {
        Task<Guid> GetPicIdGivenPortnumberAndRaspberryId(int portnumber,int buildingnumber);

        Task UpdatePicStatus(bool status,Guid id);

    }
}
