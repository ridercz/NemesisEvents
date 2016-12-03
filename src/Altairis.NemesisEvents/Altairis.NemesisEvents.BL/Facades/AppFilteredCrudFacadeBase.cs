using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class AppFilteredCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO, TFilterDTO> : FilteredCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO, TFilterDTO> 
        where TEntity : IEntity<TKey>
        where TDetailDTO : IEntity<TKey>
    {
        public AppFilteredCrudFacadeBase(IFilteredQuery<TListDTO, TFilterDTO> query, IRepository<TEntity, TKey> repository, IEntityDTOMapper<TEntity, TDetailDTO> mapper) : base(query, repository, mapper)
        {
        }
    }
}