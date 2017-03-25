namespace Altairis.NemesisEvents.BL.Facades {
    public interface ICrudFacade<TDto, TKey> {

        TDto InitializeNew();

        void Save(TDto item);

        TDto GetDetail(TKey id);

        void Delete(TKey id);


    }
}
