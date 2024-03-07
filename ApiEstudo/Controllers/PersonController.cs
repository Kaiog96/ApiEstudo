namespace ApiEstudo.Controllers
{
    using ApiEstudo.Business;
    using ApiEstudo.Model;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    { 
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id)
        {
            var person = _personBusiness.FindByID(id);

            if(person == null) { return NotFound(); }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }

            return Ok(_personBusiness.Create(person));
        }


        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }

            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);

            return NoContent();
        }
    }
}
