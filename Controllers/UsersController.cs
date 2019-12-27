using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AcimaDosOnze_Oficial.AuthenticationSector;
using AcimaDosOnze_Oficial.AuthenticationSector.Services;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;
using AcimaDosOnze_Oficial.AuthenticationSector.Models;
using AcimaDosOnze_Oficial.WeatherSector.Models;
using AcimaDosOnze_Official.WeatherSector.Services;

namespace AcimaDosOnze_Oficial.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private const bool Reproved = false;
        private const bool Approved = true;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpPost("readFavoriteIcao")]
        public IActionResult LerIcaoFavorito([FromBody]AuthenticateModel model)
        {
            var user = HttpContext.User;
            var favorite = new Favorite();
            
            if (_userService.ValidateToken(model.Username) == false || _userService.VerifyToken(model.Username) == false)
            {
                return Unauthorized("Acesso não autorizado, faça login novamente");
            }
            else if (favorite.ReadFavoriteIcao(model.Username) == null)
            {
                return NotFound("Aeródromo não encontrado");
            }

            return Ok(favorite.ReadFavoriteIcao(model.Username));
        }

        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpPost("postFavoriteIcao")]
        public IActionResult FavoritarIcao([FromBody]FavoriteIcaoModel model)
        {
            if ( _userService.ValidateToken(model.Username) == false || _userService.VerifyToken(model.Username) == false)
            {
                return Unauthorized("Acesso não autorizado, faça login novamente");
            }

            var favorite = new Favorite();

            return Ok(favorite.AddFavoriteIcao(model.Username, model.Icao.ToString()));
        }

        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpPut("updateFavoriteIcao")]
        public IActionResult UpdateFavoriteIcao([FromBody]FavoriteIcaoModel model)
        {
            if (_userService.ValidateToken(model.Username) == false || _userService.VerifyToken(model.Username) == false)
            {
                return Unauthorized("Acesso não autorizado, faça login novamente");
            }

            var favorite = new Favorite();

            return Ok(favorite.UpdateFavoriteIcao(model.Username, model.Icao));
        }
        
        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpDelete("removeFavoriteIcao")]
        public IActionResult RemoveFavoriteIcao([FromBody]User model)
        {
            //verificar se o token tá existente na tabela de usuário
            if (_userService.ValidateToken(model.UserName) == false || _userService.VerifyToken(model.UserName) == false)
            {
                return Unauthorized("Acesso não autorizado, faça login novamente");
            }

            var favorite = new Favorite();

            return Ok(favorite.RemoveFavoriteIcao(model.UserName));
        }

        [Authorize(Roles = Role.User+","+Role.Admin)]
        [HttpPost("decoder")]
        public string Get([FromBody]MetarModel model)
        {
            if (string.IsNullOrEmpty(model.Metar))
            {
                return "Metar vazio, por favor tente novamente.";
            }
            else
            {
                var airportListWeather = new TranslateMetar();
                
                return airportListWeather.Translate(model.Metar);
            }
        }

    }
}