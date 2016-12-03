using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Facades {
    public class EventsFacade : AppCrudFacadeBase<Event, int, EventDTO, EventDetailDTO> {
        public EventsFacade(EventsQuery query, IRepository<Event, int> repository, Riganti.Utils.Infrastructure.Services.Facades.IEntityDTOMapper<Event, EventDetailDTO> mapper) : base(query, repository, mapper) {
        }

        public Func<EventsQuery> EventsQueryFactory { get; set; }

        public Func<OrganizedEventsQuery> OrganizedEventsQueryFactory { get; set; }

        public IList<OrganizedEventDTO> ListEventsOrganizedBy(int userId) {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.OrganizedEventsQueryFactory();
                q.UserId = userId;
                return q.Execute();
            }
        }

    }
}
