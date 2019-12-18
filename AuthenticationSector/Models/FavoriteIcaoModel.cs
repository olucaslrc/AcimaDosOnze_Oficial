using System.ComponentModel.DataAnnotations;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Models
{
    public class FavoriteIcaoModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Icao { get; set; }
    }
}