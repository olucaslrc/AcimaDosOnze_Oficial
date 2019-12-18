using System.ComponentModel.DataAnnotations;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}