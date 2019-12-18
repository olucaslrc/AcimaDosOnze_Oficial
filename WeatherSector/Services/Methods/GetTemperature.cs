using System;
using System.Linq;

namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetTemperature
    {
        public string GetTemperatureMetar(string Metar)
        {
            var a = Metar.Reverse().ToArray();

            string b = new string(a);
            string c = new string(b.Substring(b.IndexOf("/")).Substring(1, 2));
            string temperature = new string(c.ToCharArray().Reverse().ToArray());

            return temperature;
        }
    }
}