using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class PublicEventsFacade : AppFacadeBase {

        public UpcomingEventsQuery Query { get; set; }

        public IList<UpcomingEventDTO> GetUpcomingEvents() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.Query.Execute();
            }
        }

    }
}