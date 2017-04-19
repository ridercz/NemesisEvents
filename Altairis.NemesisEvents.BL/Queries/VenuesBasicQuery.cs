using System.Linq;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries
{
    public class VenuesBasicQuery : AppQueryBase<VenueBasicDTO>
    {
        public VenuesBasicQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<VenueBasicDTO> GetQueryable()
        {
            return Context.Venues
                .ProjectTo<VenueBasicDTO>()
                .OrderBy(u => u.City)
                .ThenBy(u => u.Name);
        }
    }
}