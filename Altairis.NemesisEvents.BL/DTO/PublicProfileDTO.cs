using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {
    public class PublicProfileDTO  {

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        public EmailFrequency EmailFrequency { get; set; }

        public IList<int> WatchedAreaIds { get; set; }

        public IList<int> WatchedTagIds { get; set; }

    }
}
