using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class CurrentUserProvider : ICurrentUserProvider<int>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsInRole(string roleName)
        {
            return httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }

        public int Id => Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string UserName => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        public string DisplayName => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        public string Email => UserName;
    }
}
