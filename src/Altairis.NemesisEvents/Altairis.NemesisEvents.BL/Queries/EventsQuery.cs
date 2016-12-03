using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class EventsQuery : AppQueryBase<EventDTO>, IFilteredQuery<EventDTO> {
        public EventsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<EventDTO> GetQueryable() {
            return this.Context.Events
                .OrderByDescending(x => x.DateBegin)
                .ProjectTo<EventDTO>();
        }
    }
}
