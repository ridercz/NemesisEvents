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
        private const string ROLE_ADM = "Administrators";
        private const string ROLE_ORG = "Organizers";

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
                Email = usr.Email,
                CompanyName = usr.CompanyName,
                FullName = usr.FullName,
                Enabled = usr.Enabled,
                IsAdministrator = await mgr.IsInRoleAsync(usr, ROLE_ADM),
                IsOrganizer = await mgr.IsInRoleAsync(usr, ROLE_ORG)
            };
        }

        public async Task Save(UserDetailDTO item) {
            var mgr = this.AppUserManagerFactory();
            var usr = await mgr.FindByIdAsync(item.Id.ToString());

            // Enable or disable user
            usr.Enabled = item.Enabled;
            await mgr.UpdateAsync(usr);

            // Update roles
            await mgr.RemoveFromRolesAsync(usr, new string[] { ROLE_ADM, ROLE_ORG });
            if (item.IsAdministrator) await mgr.AddToRoleAsync(usr, ROLE_ADM);
            if (item.IsOrganizer) await mgr.AddToRoleAsync(usr, ROLE_ORG);
        }

    }
}
