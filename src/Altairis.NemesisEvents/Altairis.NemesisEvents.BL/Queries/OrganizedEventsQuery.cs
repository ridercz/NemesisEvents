using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries {
    public class OrganizedEventsQuery : AppQueryBase<OrganizedEventDTO> {
        public OrganizedEventsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        public int UserId { get; set; }

        protected override IQueryable<OrganizedEventDTO> GetQueryable() {
            return this.Context.Events
                .Where(x => x.OwnerId == this.UserId)
                .OrderByDescending(x => x.DateBegin)
                .ProjectTo<OrganizedEventDTO>();
        }
    }
}
