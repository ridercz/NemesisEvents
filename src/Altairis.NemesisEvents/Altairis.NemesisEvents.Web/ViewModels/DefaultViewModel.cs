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

        public IList<PublicUpcomingEventDTO> UpcomingEvents { get; set; }

        public DefaultViewModel(PublicEventsFacade f) {
            this.UpcomingEvents = f.GetUpcomingEvents();
        }

    }
}
