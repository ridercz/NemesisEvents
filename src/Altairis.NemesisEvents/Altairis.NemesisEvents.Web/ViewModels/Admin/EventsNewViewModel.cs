using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;

namespace Altairis.NemesisEvents.Web.ViewModels.Admin {
    public class EventsNewViewModel : NewViewModelBase<EventsFacade, EventDetailDTO, int> {

        // Injected services

        [Bind(Direction.None)]
        public BaseListsFacade BaseListsFacade { get; set; }

        // Page infrastructure

        protected override string ContinuePageRouteName => "AdminEvents";

        protected override string GetPageTitle() => "Vytvoøení nové akce";

        // Binding properties

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<UserBasicDTO> Users => this.BaseListsFacade.GetOrganizerOrAdminUsers();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<VenueBasicDTO> Venues => this.BaseListsFacade.GetVenues();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<TagDTO> Tags => this.BaseListsFacade.GetTags();


    }
}

