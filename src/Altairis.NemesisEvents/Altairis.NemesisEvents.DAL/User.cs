using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Altairis.NemesisEvents.DAL {
    public class User : IdentityUser<int> {

        public bool SendSingleMessages { get; set; }

        public bool SendDailyMessages { get; set; }

        public bool SendWeeklyMessages { get; set; }

        public virtual ICollection<Event> OwnedEvents { get; set; }

    }
}
