namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Data.Converter.Implementations;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Repository;

    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _bookRepository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> bookRepository)
        {
            this._bookRepository = bookRepository;
            this._converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return this._converter.Parse(_bookRepository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return this._converter.Parse(_bookRepository.FindByID(id));
        }

        public BookVO Create(BookVO bookVO)
        {
            var bookEntity = this._converter.Parse(bookVO);

            bookEntity = this._bookRepository.Create(bookEntity);

            return this._converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO bookVO)
        {
            var bookEntity = this._converter.Parse(bookVO);

            bookEntity = this._bookRepository.Update(bookEntity);

            return this._converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            this._bookRepository.Delete(id);
        }
    }
}
