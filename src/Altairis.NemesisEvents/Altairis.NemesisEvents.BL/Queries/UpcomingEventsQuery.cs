using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class UpcomingEventsQuery : AppQueryBase<UpcomingEventDTO> {
        public UpcomingEventsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<UpcomingEventDTO> GetQueryable() {
            return this.Context.Events
                .Where(x => x.DateEnd >= DateTime.Now)
                .OrderBy(x => x.DateBegin)
                .ProjectTo<UpcomingEventDTO>();
        }
    }
}
