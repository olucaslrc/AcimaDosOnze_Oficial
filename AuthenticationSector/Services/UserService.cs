using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AcimaDosOnze_Oficial.AuthenticationSector.Data;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;
using AcimaDosOnze_Oficial.AuthenticationSector.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Services
{
    public interface IUserService
    {
        String Authenticate(string username, string password);
        bool VerifyToken(string username);
        bool ValidateToken(string username);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string Authenticate(string username, string password)
        {
            var db = new DBContext();

            var _account = new Account();

            var login = _account.LoginAccount(username, password);
            
            var user = db.Users.FirstOrDefault(x => x.UserName == username);

            var userToken = db.Tokens.FirstOrDefault(x => x.UserId == user.Id);

            // return null if user not found
            if (login == false)
            {
                return null;
            }
            else if (userToken != null)
            {
                db.Remove(userToken);
                db.SaveChanges();
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var dataToken = tokenHandler.WriteToken(token);

            var date = tokenHandler.ReadToken(dataToken) as SecurityToken;
            var tokenExpiryDate = date.ValidTo;

            db.Add(new Token {
                UserId = user.Id,
                UserToken = dataToken,
                DateExpiryToken = tokenExpiryDate
            });

            db.SaveChanges();

            return dataToken;
        }

        public bool ValidateToken(string username)
        {
            var db = new DBContext();

            var user = db.Users.FirstOrDefault(x => x.UserName == username);

            var userToken = db.Tokens.FirstOrDefault(x => x.UserId == user.Id);
            
            if (userToken == null)
            {
                return false;
            }

            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadToken(userToken.UserToken) as SecurityToken;

            var tokenExpiryDate = token.ValidTo;
            
            if (tokenExpiryDate < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }

        public bool VerifyToken(string username)
        {
            var db = new DBContext();

            var user = db.Users.FirstOrDefault(x => x.UserName == username);

            var userToken = db.Tokens.FirstOrDefault(x => x.UserId == user.Id);

            if (String.IsNullOrEmpty(userToken.UserToken))
            {
                return false;
            }

            return true;
        }
    }
}