using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.NemesisEvents.BL.DTO {
    public class SelectListItemDTO<TValue, TDisplay> {

        public TValue Value { get; set; }

        public TDisplay Display { get; set; }

    }
}
