using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class AreasFacade : AppFacadeBase {

        public AreasQuery Query { get; set; }

        public IList<AreaDTO> GetAreas() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.Query.Execute();
            }
        }

    }
}
