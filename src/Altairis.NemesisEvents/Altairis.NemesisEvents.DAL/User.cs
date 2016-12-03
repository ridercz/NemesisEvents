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

        public EmailFrequency EmailFrequency { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastLogin { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<Event> OwnedEvents { get; } = new HashSet<Event>();

        public virtual ICollection<Attendee> Attendances { get; } = new HashSet<Attendee>();

        public virtual ICollection<UserArea> WatchedAreas { get; } = new HashSet<UserArea>();

        public virtual ICollection<UserTag> WatchedTags { get; } = new HashSet<UserTag>();

    }

    public enum EmailFrequency {
        None = 0,
        Separate = 1,
        Daily = 2,
        Weekly = 3
    }
}
