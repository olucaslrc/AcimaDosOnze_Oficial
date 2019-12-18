namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetDewPoint
    {
        public string GetDewPointMetar(string Metar)
        {
            string dewPoint = Metar.Substring(Metar.IndexOf("/"), 3).Substring(1);

            return dewPoint;
        }
    }
}