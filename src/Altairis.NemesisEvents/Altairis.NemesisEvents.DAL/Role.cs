using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.DAL {
    public class Role : IdentityRole<int>, IEntity<int> {
    }
}
