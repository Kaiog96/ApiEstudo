namespace ApiEstudo.Controllers
{
    using ApiEstudo.Business;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Hypermedia.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize("Bearer")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            this._logger = logger;
            this._bookBusiness = bookBusiness;
        }

        [HttpGet]
        [Route("Book", Name = "GetBooks")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter (typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet]
        [Route("Book/{id}", Name = "GetBook")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetFindByID(long id)
        {
            var book = _bookBusiness.FindByID(id);

            if (book == null) { return NotFound(); }

            return Ok(book);
        }

        [HttpPost]
        [Route("Book", Name = "PostBook")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO bookVO)
        {
            if (bookVO == null) { return BadRequest(); }

            return Ok(_bookBusiness.Create(bookVO));
        }

        [HttpPut]
        [Route("Book", Name = "PutBook")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO bookVO)
        {
            if (bookVO == null) { return BadRequest(); }

            return Ok(_bookBusiness.Update(bookVO));
        }

        [HttpDelete]
        [Route("Book/{id}", Name = "DeleteBook")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
    }
}
