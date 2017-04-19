using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.DTO;

namespace Altairis.NemesisEvents.Web.ViewModels.Admin
{
    public class EventsViewModel : GridViewModelBase<EventsFacade, EventDTO> {
        protected override string GetPageTitle() => "Seznam akcí";
    }
}

