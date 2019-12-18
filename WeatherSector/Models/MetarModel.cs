using System.ComponentModel.DataAnnotations;

namespace AcimaDosOnze_Oficial.WeatherSector.Models
{
    public class MetarModel
    {
        [Required]
        public string Metar { get; set; }
    }
}