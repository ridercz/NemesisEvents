using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class AttachmentDTO : IEntity<int> {

        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string StorageUrl { get; set; }

        public AttachmentType Type { get; set; }

    }
}
