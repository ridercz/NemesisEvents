using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Queries
{
    public class TagsQuery : AppQueryBase<TagDTO> {
        public TagsQuery(IUnitOfWorkProvider provider) : base(provider) {
        }

        protected override IQueryable<TagDTO> GetQueryable() {
            return this.Context.Tags.ProjectTo<TagDTO>();
        }
    }
}
