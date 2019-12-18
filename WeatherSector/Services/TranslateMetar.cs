using AcimaDosOnze_Oficial.Services.WeatherServices.Methods;

namespace AcimaDosOnze_Official.WeatherSector.Services
{
    public class TranslateMetar
    {
        public string Translate(string Metar)
        {
            var a = new AirportListWeather();

            return a.GetWeatherInfo(Metar);
        }
    }
}