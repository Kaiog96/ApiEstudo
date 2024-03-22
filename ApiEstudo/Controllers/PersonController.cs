namespace ApiEstudo.Controllers
{
    using ApiEstudo.Business;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Hypermedia.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    public class PersonController : ControllerBase
    { 
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            this._logger = logger;
            this._personBusiness = personBusiness;
        }

        [HttpGet]
        [Route("Person/{name}/{sortDirection}/{pageSize}/{page}", Name = "GetPersons")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(string name, string sortDirection, int pageSize, int page)
        { 
            return Ok(this._personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        [HttpGet]
        [Route("Person/{id}", Name = "GetPerson")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetFindByID(long id)
        {
            var person = this._personBusiness.FindByID(id);

            if(person == null) { return NotFound(); }

            return Ok(person);
        }

        [HttpGet]
        [Route("Person/FindByName/{firstName}/{lastName}", Name = "GetPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetFindByName(string firstName, string lastName)
        {
            var person = this._personBusiness.FindByName(firstName, lastName);

            if (person == null) { return NotFound(); }

            return Ok(person);
        }

        [HttpPost]
        [Route("Person", Name = "PostPerson")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO personVO)
        {
            if (personVO == null) { return BadRequest(); }

            return Ok(this._personBusiness.Create(personVO));
        }
        
        [HttpPut]
        [Route("Person", Name = "PutPerson")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO personVO)
        {
            if (personVO == null) { return BadRequest(); }

            return Ok(this._personBusiness.Update(personVO));
        }

        [HttpPatch]
        [Route("Person/{id}", Name = "PatchPerson")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = this._personBusiness.Disable(id);

            return Ok(person);
        }

        [HttpDelete]
        [Route("Person/{id}", Name = "DeletePerson")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Delete(long id)
        {
            this._personBusiness.Delete(id);

            return NoContent();
        }
    }
}
