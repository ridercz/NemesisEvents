using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class PublicEventsFacade : AppFacadeBase {

        public UpcomingEventsQuery UpcomingQuery { get; set; }

        public ArchiveEventsQuery ArchiveQuery { get; set; }

        public IList<UpcomingEventDTO> GetUpcomingEvents() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.UpcomingQuery.Execute();
            }
        }

        public IList<ArchiveEventDTO> GetArchiveEvents() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.ArchiveQuery.Execute();
            }
        }

    }
}