using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.Queries {
    public class UpcomingEventDTO {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public string VenueDisplayName => string.Join(" - ", this.VenueCity, this.VenueName);

        public string OrganizerName { get; set; }

        public bool IsFree { get; set; }

        public IEnumerable<string> Tags { get; set;  }

        public string TagsDisplayText => string.Join(", ", this.Tags);

    }
}