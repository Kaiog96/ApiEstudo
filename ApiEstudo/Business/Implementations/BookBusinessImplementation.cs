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
            _bookRepository = bookRepository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_bookRepository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse(_bookRepository.FindByID(id));
        }

        public BookVO Create(BookVO bookVO)
        {
            var bookEntity = _converter.Parse(bookVO);

            bookEntity = _bookRepository.Create(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO bookVO)
        {
            var bookEntity = _converter.Parse(bookVO);

            bookEntity = _bookRepository.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _bookRepository.Delete(id);
        }
    }
}
