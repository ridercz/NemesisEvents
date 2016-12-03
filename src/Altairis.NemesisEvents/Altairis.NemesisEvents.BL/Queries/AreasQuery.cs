using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries
{
    public class AreasQuery : AppQueryBase<AreaDTO> {
        public AreasQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<AreaDTO> GetQueryable() {
            return this.Context.Areas
                .OrderBy(a => a.Name)
                .ProjectTo<AreaDTO>();
        }
    }
}
