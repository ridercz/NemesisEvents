using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.BL.Services;
using AutoMapper;

namespace Altairis.NemesisEvents.BL.Facades {
    public class UsersFacade : AppFacadeBase {

        public Func<AppUserManager> AppUserManagerFactory { get; set; }

        public Func<UsersQuery> UsersQueryFactory { get; set; }

        public IList<UserDTO> GetList(bool includeDisabled = false, bool includeUnconfirmed = false) {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.UsersQueryFactory();
                q.IncludeDisabled = includeDisabled;
                q.IncludeUnconfirmed = includeUnconfirmed;
                return q.Execute();
            }
        }

        public async Task<UserDetailDTO> GetDetail(int userId) {
            var mgr = this.AppUserManagerFactory();
            var usr = await mgr.FindByIdAsync(userId.ToString());
            return new UserDetailDTO {
                Id = usr.Id,
                CompanyName = usr.CompanyName,
                Email = usr.Email,
                Enabled = usr.Enabled,
                FullName = usr.FullName,
                IsAdministrator = await mgr.IsInRoleAsync(usr, "Administrators"),
                IsOrganizer = await mgr.IsInRoleAsync(usr, "Organizers")
            };
        }

        public void Save(UserDTO item) {
            throw new NotImplementedException();
        }



    }
}
