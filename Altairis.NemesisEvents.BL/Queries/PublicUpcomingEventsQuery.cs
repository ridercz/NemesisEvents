using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class PublicUpcomingEventsQuery : AppQueryBase<PublicUpcomingEventDTO> {
        public PublicUpcomingEventsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        public int AttendingUserId { get; set; }

        protected override IQueryable<PublicUpcomingEventDTO> GetQueryable() {
            var events = this.Context.Events.Where(x => x.DateEnd >= DateTime.Now);
            if (this.AttendingUserId > 0) events = events.Where(x => x.Attendees.Any(a => a.UserId == this.AttendingUserId));
            return events.OrderBy(x => x.DateBegin).ProjectTo<PublicUpcomingEventDTO>();
        }
    }
}
