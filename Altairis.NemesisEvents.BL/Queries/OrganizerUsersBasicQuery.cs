using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries
{
    public class OrganizerUsersBasicQuery : AppQueryBase<UserBasicDTO>
    {
        public OrganizerUsersBasicQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<UserBasicDTO> GetQueryable()
        {
            return Context.Users
                .Where(u => u.Roles.Any())
                .ProjectTo<UserBasicDTO>()
                .OrderBy(u => u.FullName);
        }
    }
}

