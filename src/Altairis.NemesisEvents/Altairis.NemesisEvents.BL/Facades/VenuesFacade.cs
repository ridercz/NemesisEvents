using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class VenuesFacade : AppFacadeBase {

        public VenuesQuery Query { get; set; }

        public IList<VenueDTO> List() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.Query.Execute();
            }
        }

    }
}
