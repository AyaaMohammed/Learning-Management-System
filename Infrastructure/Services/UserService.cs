using Application.Interfaces.UserService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue("userId");

                return Guid.Parse(value!);
            }
        }

        public Guid TenantId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue("tenantId");

                return Guid.Parse(value!);
            }
        }
    }
}
