using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;

namespace Altairis.NemesisEvents.BL.Facades {
    public class AreasFacade : AppCrudFacadeBase<Area, int, AreaDTO, AreaDTO> {

        public AreasFacade(Func<IQuery<AreaDTO>> queryFactory, IRepository<Area, int> repository, IEntityDTOMapper<Area, AreaDTO> mapper)
            : base(queryFactory, repository, mapper) { }


    }
}
