using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UserService
{
    public interface IUserService
    {
        Guid UserId { get; }
        Guid TenantId { get; }
    }
}
