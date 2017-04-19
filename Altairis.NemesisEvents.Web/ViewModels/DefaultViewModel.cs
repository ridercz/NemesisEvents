using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Queries;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels {
    public class DefaultViewModel : MasterPageViewModel {

        private readonly PublicEventsFacade facade;

        public IList<PublicUpcomingEventDTO> UpcomingEvents { get; set; }

        public IList<PublicArchiveEventDTO> RecentEvents { get; set; }


        public DefaultViewModel(PublicEventsFacade facade)
        {
            this.facade = facade;
        }


        public override Task PreRender()
        {
            this.UpcomingEvents = facade.ListUpcomingEvents();
            this.RecentEvents = facade.ListRecentArchiveEvents();
            return base.PreRender();
        }
    }
}
