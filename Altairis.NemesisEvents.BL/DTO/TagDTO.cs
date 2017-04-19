using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class TagDTO : IEntity<int> {

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
