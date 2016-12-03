using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class EventsFacade : AppFacadeBase {

        public Func<EventsQuery> EventsQueryFactory { get; set; }

        public Func<OrganizedEventsQuery> OrganizedEventsQueryFactory { get; set; }

        public IList<EventDTO> List() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.EventsQueryFactory().Execute();
            }
        }

        public IList<OrganizedEventDTO> ListEventsOrganizedBy(int userId) {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.OrganizedEventsQueryFactory();
                q.UserId = userId;
                return q.Execute();
            }
        }

    }
}
