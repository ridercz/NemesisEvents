using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.ViewModel;
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.Web.ViewModels.My {
    public class DefaultViewModel : Altairis.NemesisEvents.Web.ViewModels.MasterPageViewModel {
        private readonly PublicEventsFacade publicEventsFacade;
        private readonly ICurrentUserProvider<int> currentUser;
        private readonly EventsFacade eventsFacade;

        public IList<PublicUpcomingEventDTO> UpcomingEvents { get; private set; }
        public IList<OrganizedEventDTO> UpcomingOrganizedEvents { get; private set; }

        public DefaultViewModel(ICurrentUserProvider<int> currentUser, PublicEventsFacade publicEventsFacade, EventsFacade eventsFacade) {
            this.currentUser = currentUser;
            this.publicEventsFacade = publicEventsFacade;
            this.eventsFacade = eventsFacade;
        }

        public async override Task PreRender() {
            this.UpcomingEvents = this.publicEventsFacade.ListUpcomingEvents(this.currentUser.Id);
            this.UpcomingOrganizedEvents = this.eventsFacade.ListEventsOrganizedBy();
        }


    }
}

