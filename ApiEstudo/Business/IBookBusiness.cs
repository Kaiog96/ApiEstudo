namespace ApiEstudo.Business
{
    using ApiEstudo.Data.VO;

    public interface IBookBusiness
    {
        List<BookVO> FindAll();

        BookVO FindByID(long id);

        BookVO Create(BookVO bookVO);

        BookVO Update(BookVO bookVO);

        void Delete(long id);
    }
}
