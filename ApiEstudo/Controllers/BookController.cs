namespace ApiEstudo.Controllers
{
    using ApiEstudo.Business;
    using ApiEstudo.Data.VO;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id)
        {
            var book = _bookBusiness.FindByID(id);

            if (book == null) { return NotFound(); }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO bookVO)
        {
            if (bookVO == null) { return BadRequest(); }

            return Ok(_bookBusiness.Create(bookVO));
        }


        [HttpPut]
        public IActionResult Put([FromBody] BookVO bookVO)
        {
            if (bookVO == null) { return BadRequest(); }

            return Ok(_bookBusiness.Update(bookVO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
    }
}
