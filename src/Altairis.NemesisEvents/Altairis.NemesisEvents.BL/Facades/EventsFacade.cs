using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class EventsFacade : AppFacadeBase {

        public EventQuery Query { get; set; }

        public IList<EventDTO> List() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.Query.Execute();
            }
        }

    }
}
