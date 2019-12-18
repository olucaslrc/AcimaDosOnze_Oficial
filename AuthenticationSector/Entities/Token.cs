using System;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserToken { get; set; }
        public DateTime DateExpiryToken { get; set; }
    }
}