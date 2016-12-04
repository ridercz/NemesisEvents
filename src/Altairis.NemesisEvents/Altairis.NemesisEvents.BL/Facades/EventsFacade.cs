using System;
using System.Collections.Generic;
using System.Security;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.BL.Facades {
    public class EventsFacade : AppCrudFacadeBase<Event, int, EventDTO, EventDetailDTO> {

        public EventsFacade(Func<EventsQuery> queryFactory, IRepository<Event, int> repository, Riganti.Utils.Infrastructure.Services.Facades.IEntityDTOMapper<Event, EventDetailDTO> mapper) : base(queryFactory, repository, mapper) {
        }

        public Func<OrganizedEventsQuery> OrganizedEventsQueryFactory { get; set; }

        public ICurrentUserProvider<int> CurrentUser { get; set; }

        public override void Save(EventDetailDTO detail) {
            // Force current user ID to non-admins
            if (!this.CurrentUser.IsInRole(Role.Administrators)) detail.OwnerId = this.CurrentUser.Id;

            base.Save(detail);
        }

        public override IEnumerable<EventDTO> GetList() {
            if (!this.CurrentUser.IsInRole(Role.Administrators)) throw new SecurityException("Only administrators can access events owned by other users.");

            return base.GetList();
        }

        public override EventDetailDTO GetDetail(int id) {
            var d = base.GetDetail(id);
            if (d.OwnerId != this.CurrentUser.Id && !this.CurrentUser.IsInRole(Role.Administrators)) throw new SecurityException("Only administrators can access events owned by other users.");
            return d;
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
