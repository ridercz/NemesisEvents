using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class AppCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO> : CrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO>
        where TEntity : IEntity<TKey>
        where TDetailDTO : IEntity<TKey> {
        public AppCrudFacadeBase(IQuery<TListDTO> query, IRepository<TEntity, TKey> repository, IEntityDTOMapper<TEntity, TDetailDTO> mapper) : base(query, repository, mapper) {
        }
    }
}
