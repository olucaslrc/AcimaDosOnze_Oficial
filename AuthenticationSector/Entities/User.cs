using System.Collections.Generic;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Token> Token { get; set; }
        public ICollection<Icao> Icao { get; set; }
    }
}