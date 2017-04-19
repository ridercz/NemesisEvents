using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.BL.DTO {
    public class EventDTO {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBegin { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public string VenueDisplayName => string.Join(" - ", this.VenueCity, this.VenueName);

        public bool UseRegistration { get; set; }

        public bool AllowRegistration { get; set; }

        public string OrganizerName { get; set; }

        public bool HasAdmissionFee { get; set; }

        public DateTime DateCreated { get; set; }

        public bool InvitationSent { get; set; }

        public int OwnerId { get; set; }

        public string OwnerFullName { get; set; }

        public string OwnerCompanyName { get; set; }

        public string OwnerDisplayName => string.Join(", ", this.OwnerFullName, this.OwnerCompanyName);

        public int AttendeesCount { get; set; }

    }
}
