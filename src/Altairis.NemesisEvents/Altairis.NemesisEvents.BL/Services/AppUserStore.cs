using Altairis.NemesisEvents.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace Altairis.NemesisEvents.BL.Services
{
    public class AppUserStore : UserStore<User, Role, NemesisEventsContext, int>
    {
        public AppUserStore(IUnitOfWorkProvider provider, IdentityErrorDescriber describer = null) 
            : base((NemesisEventsContext)EntityFrameworkUnitOfWork.TryGetDbContext(provider), describer)
        {
            this.AutoSaveChanges = false;
        }
    }
}