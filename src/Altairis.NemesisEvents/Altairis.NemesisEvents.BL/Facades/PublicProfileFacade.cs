using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Services;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.BL.Facades {
    public class PublicProfileFacade : AppFacadeBase {

        public Func<AppUserManager> AppUserManagerFactory { get; set; }

        public ICurrentUserProvider<int> CurrentUser { get; set; }

        public async Task<PublicProfileDTO> GetMyProfile() {
            var mgr = this.AppUserManagerFactory();
            var usr = await mgr.FindByIdAsync(this.CurrentUser.Id.ToString());
            return Mapper.Map<PublicProfileDTO>(usr);
        }

        public async Task SaveMyProfile(PublicProfileDTO item) {
            var mgr = this.AppUserManagerFactory();
            var usr = await mgr.FindByIdAsync(this.CurrentUser.Id.ToString());

            Mapper.Map(item, usr);
            await mgr.UpdateAsync(usr);
        }

    }
}
