using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels.My {
    public class ProfileViewModel : Altairis.NemesisEvents.Web.ViewModels.MasterPageViewModel {

        private PublicProfileFacade profileFacade;
        private AreasFacade areasFacade;
        private TagsFacade tagsFacade;

        public PublicProfileDTO Item { get; set; }

        public IEnumerable<AreaDTO> Areas => this.areasFacade.GetList();

        public IEnumerable<TagDTO> Tags => this.tagsFacade.GetList();

        public ProfileViewModel(PublicProfileFacade profileFacade, AreasFacade areasFacade, TagsFacade tagsFacade) {
            this.profileFacade = profileFacade;
            this.areasFacade = areasFacade;
            this.tagsFacade = tagsFacade;
        }

        public async override Task PreRender() {
            this.Item = await profileFacade.GetMyProfileAsync();
        }

        public async Task Save() {
            await ExecuteSafeAsync(async () => await profileFacade.SaveMyProfileAsync(this.Item));
            this.Context.RedirectToRoute("MyEvents");
        }


    }
}

