using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class PublicEventsFacade : AppFacadeBase {

        public PublicUpcomingEventsQuery UpcomingQuery { get; set; }

        public PublicArchiveEventsQuery ArchiveQuery { get; set; }

        public IList<PublicUpcomingEventDTO> ListUpcomingEvents() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.UpcomingQuery.Execute();
            }
        }

        public IList<PublicArchiveEventDTO> ListArchiveEvents() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.ArchiveQuery.Execute();
            }
        }

    }
}