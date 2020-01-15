using System;

namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetPression
    {
        public string GetPressionMetar(string Metar)
        {
            try
            {
                string result = string.Empty;

                var PressionStr = Metar.Substring(Metar.IndexOf("/"));

                var Pression = PressionStr.Substring(PressionStr.IndexOf("Q")).Substring(1, 4);

                int testInt = int.Parse(Pression);

                result = Pression;

                return $"QNH: {result} hPa";  
            }
            catch (System.Exception Exception)
            {
                Console.WriteLine(  $"\n___________________________________________________________________\n" +
                                    $"\nData: {DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss")}\n" +
                                    $"\nClasse:       GetPression\n" +
                                    $"\nMétodo:       GetPressionMetar()\n" +
                                    $"\nExceção executada, verifique-a:\n\n{Exception}" +
                                    $"\n___________________________________________________________________\n" );

                return "Não foi possível decodificar a pressão";
            }
            
        }
    }
}