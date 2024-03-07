namespace ApiEstudo.Controllers
{
    using ApiEstudo.Model;
    using ApiEstudo.Services;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    { 
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id)
        {
            var person = _personService.FindByID(id);

            if(person == null) { return NotFound(); }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }

            return Ok(_personService.Create(person));
        }


        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }

            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);

            return NoContent();
        }
    }
}
