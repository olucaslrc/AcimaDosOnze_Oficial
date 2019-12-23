namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetDewPoint
    {
        public string GetDewPointMetar(string Metar)
        {
            try
            {
                string dewPoint = Metar.Substring(Metar.IndexOf("/"), 3).Substring(1);
                int dP = int.Parse(dewPoint);

                return dP.ToString();
            }
            catch (System.Exception)
            {
                return "Não foi possível decodificar o ponto de orvalho."; 
            }
            
        }
    }
}