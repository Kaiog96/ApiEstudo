namespace ApiEstudo.Controllers
{
    using ApiEstudo.Business;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Hypermedia.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin", Name = "PostAuth")]
        public IActionResult Signin([FromBody] UserVO userVO)
        {
            if (userVO == null) return BadRequest("Invalid cliente request");

            var token = this._loginBusiness.ValidateCredentials(userVO);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh", Name = "PostRefresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid cliente request");

            var token = this._loginBusiness.ValidateCredentials(tokenVO);

            if (token == null) return BadRequest("Invalid cliente request");

            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke", Name = "PostRevoke")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;

            var result = this._loginBusiness.RevokeToken(userName);

            if (!result == null) return BadRequest("Invalid cliente request");

            return NoContent();
        }
    }
}
