namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetHour
    {
        public string ConvertHourMetar(string Metar)
        {
            var dateHH = Metar.Substring(8, 2);

            return $"{dateHH} horas (UTC)";
        }
    }
}