using System;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;

namespace Altairis.NemesisEvents.BL.Facades {
    public class VenuesFacade : AppCrudFacadeBase<Venue, int, VenueDTO, VenueDetailDTO> {
        public VenuesFacade(Func<IQuery<VenueDTO>> queryFactory, IRepository<Venue, int> repository, IEntityDTOMapper<Venue, VenueDetailDTO> mapper)
            : base(queryFactory, repository, mapper) { }
    }
}
