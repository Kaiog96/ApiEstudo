namespace ApiEstudo.Business
{
    using ApiEstudo.Model;

    public interface IBookBusiness
    {
        List<Book> FindAll();

        Book FindByID(long id);

        Book Create(Book book);

        Book Update(Book book);

        void Delete(long id);
    }
}
