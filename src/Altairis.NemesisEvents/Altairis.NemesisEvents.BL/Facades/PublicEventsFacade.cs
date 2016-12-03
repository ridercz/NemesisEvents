﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class PublicEventsFacade : AppFacadeBase {

        public Func<PublicUpcomingEventsQuery> UpcomingQueryFactory { get; set; }

        public Func<PublicArchiveEventsQuery> ArchiveQueryFactory { get; set; }

        public IList<PublicUpcomingEventDTO> ListUpcomingEvents(int attendingUserId = 0) {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.UpcomingQueryFactory();
                if (attendingUserId > 0) q.AttendingUserId = attendingUserId;
                return q.Execute();
            }
        }

        public IList<PublicArchiveEventDTO> ListArchiveEvents(int attendingUserId = 0) {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.ArchiveQueryFactory();
                if (attendingUserId > 0) q.AttendingUserId = attendingUserId;
                return q.Execute();
            }
        }

    }
}