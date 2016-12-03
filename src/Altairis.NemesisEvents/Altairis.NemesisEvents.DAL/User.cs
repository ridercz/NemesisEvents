using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Altairis.NemesisEvents.DAL {
    public class User : IdentityUser<int> {

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        public bool SendSingleMessages { get; set; }

        public bool SendDailyMessages { get; set; }

        public bool SendWeeklyMessages { get; set; }

        public virtual ICollection<Event> OwnedEvents { get; } = new HashSet<Event>();

        public virtual ICollection<Attendee> Attendances { get; } = new HashSet<Attendee>();

        public virtual ICollection<UserArea> WatchedAreas { get; } = new HashSet<UserArea>();

        public virtual ICollection<UserTag> WatchedTags { get; } = new HashSet<UserTag>();

    }
}
