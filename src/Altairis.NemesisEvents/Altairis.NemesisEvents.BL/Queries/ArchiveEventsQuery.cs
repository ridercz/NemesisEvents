using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class ArchiveEventsQuery : AppQueryBase<ArchiveEventDTO> {
        public ArchiveEventsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<ArchiveEventDTO> GetQueryable() {
            return this.Context.Events
                .Where(x => x.DateEnd <= DateTime.Now)
                .OrderByDescending(x => x.DateEnd)
                .ProjectTo<ArchiveEventDTO>();
        }
    }
}
