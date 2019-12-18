namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class AirportListWeather
    {
        private SerializeMetar SM = new SerializeMetar();

        public string GetWeatherInfo(string Metar)
        {
            return SM.GetJson(Metar);
        }
        
    }
}