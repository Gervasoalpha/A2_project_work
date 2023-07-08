using A2_project_work.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Token GetToken(string username, Guid userID, bool  isadmin=false, bool israsp=false);
    }
}
