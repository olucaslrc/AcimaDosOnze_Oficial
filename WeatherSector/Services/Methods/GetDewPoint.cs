namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetDewPoint
    {
        public string GetDewPointMetar(string Metar)
        {
            try
            {
                string dewPoint = Metar.Substring(Metar.IndexOf("/"), 3).Substring(1);

                int testInt = int.Parse(dewPoint);

                return dewPoint;
            }
            catch (System.Exception)
            {
                return "Não foi possível decodificar o ponto de orvalho."; 
            }
            
        }
    }
}