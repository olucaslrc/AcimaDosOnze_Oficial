using AcimaDosOnze_Oficial.AuthenticationSector;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;
using AcimaDosOnze_Oficial.AuthenticationSector.Models;
using AcimaDosOnze_Oficial.AuthenticationSector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcimaDosOnze_Oficial.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("verifyCredentials")]
        public IActionResult VerifyCredentials([FromBody]User model)
        {
            if (_userService.ValidateToken(model.UserName) == false || _userService.VerifyToken(model.UserName) == false)
            {
                return Unauthorized("Acesso não autorizado, faça login novamente");
            }
            else
            {
                return Ok("Acesso Ok");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]User model)
        {
            var _account = new Account();

            var user = _account.CreateAccount(model.UserName, model.Email, model.Password);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody]AuthenticateModel model)
        {
            var userLogin = _userService.Authenticate(model.Username, model.Password);

            if (userLogin == null)
            {   
                return BadRequest(new { message = "Username ou senha incorretos" });
            }
            
            return Ok(userLogin);
        }

        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpPost("logout")]
        public IActionResult LogoutUser([FromBody]AuthenticateModel model)
        {
            var _account = new Account();

            if (_account.LogoutAccount(model.Username) == true)
            {
                return Ok("Deslogado com sucesso.");
            }
            else
            {
                return Unauthorized("Erro, você não tem autorização para realizar esta ação.");
            }
        }
    }
}