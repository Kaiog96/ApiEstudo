namespace ApiEstudo.Repository
{
    using ApiEstudo.Model;

    public interface IBookRepository
    {
        List<Book> FindAll();

        Book FindByID(long id);

        Book Create(Book book);

        Book Update(Book book);

        void Delete(long id);

        bool Exists(long id);
    }
}
