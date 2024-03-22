namespace ApiEstudo.Repository
{
    using ApiEstudo.Model.Base;

    public interface IRepository<T> where T : BaseEntity    
    {
        List<T> FindAll();

        T FindByID(long id);

        T Create(T item);

        T Update(T item);

        void Delete(long id);

        bool Exists(long id);

        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);
    }
}
