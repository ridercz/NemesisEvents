using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class UsersQuery : AppQueryBase<UserDTO> {
        public UsersQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        public bool IncludeDisabled { get; set; } = false;

        public bool IncludeUnconfirmed { get; set; } = false;

        protected override IQueryable<UserDTO> GetQueryable() {
            IQueryable<User> q = this.Context.Users;

            if (!this.IncludeDisabled) q = q.Where(x => x.Enabled);
            if (!this.IncludeUnconfirmed) q = q.Where(x => x.EmailConfirmed);

            return q.OrderBy(x => x.Email).ProjectTo<UserDTO>();
        }

    }
}
