using System;

namespace AcimaDosOnze_Oficial.Services.WeatherServices.Methods
{
    public class GetDate
    {
        public string[] ConvertDateMetar(string Metar)
        {
            var stringYY = Metar.Substring(0, 4);
            var stringMM = Metar.Substring(4, 2);
            var stringDD = Metar.Substring(6, 2);

            try
            {
                var YY = int.Parse(stringYY);
                var MM = int.Parse(stringMM);
                var DD = int.Parse(stringDD);

                string[] arrayResult = {
                    $"{DateTime.Parse($"{DD},{MM},{YY}").ToString("dd/MM/yy")}",
                    $"{DateTime.Parse($"{DD},{MM},{YY}").ToString("dd")} de " +
                    $"{DateTime.Parse($"{DD},{MM},{YY}").ToString("MMMM")} de " +
                    $"{DateTime.Parse($"{DD},{MM},{YY}").ToString("yyyy")}",
                };

                return arrayResult; 
            }
            catch (System.Exception Exception)
            {
                Console.WriteLine(  $"\n___________________________________________________________________\n" +
                                    $"\nExceção executada, verifique-a:\n\n{Exception}" +
                                    $"\n___________________________________________________________________\n" );

                string[] arrayResult = {
                    "Não foi possível converter a data do METAR, confira o código."
                };

                return arrayResult;
            }
        } 
    }
}