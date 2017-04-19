using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries
{
    public class VenuesQuery : AppQueryBase<VenueDTO> {
        public VenuesQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<VenueDTO> GetQueryable() {
            return this.Context.Venues
                .OrderBy(x => x.City).ThenBy(x => x.Name)
                .ProjectTo<VenueDTO>();
        }

    }
}
