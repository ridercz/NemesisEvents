using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class UsersQuery : AppQueryBase<UserDTO> {
        public UsersQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<UserDTO> GetQueryable() {
            return this.Context.Users
                .OrderBy(x => x.Email)
                .ProjectTo<UserDTO>();
        }

    }
}
