using System.Linq;
using System;
using AcimaDosOnze_Oficial.AuthenticationSector.Data;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;

namespace AcimaDosOnze_Oficial.AuthenticationSector
{
    public class Favorite
    {
        public string AddFavoriteIcao(string userName, string icao)
        {
            int lengthIcao = 4;

            if (icao == String.Empty)
            {
                return Message.MESSAGE_CAMPO_FAVORITAR_ICAO_VAZIO;
            }
            else if (icao.Length < lengthIcao || icao.Length > lengthIcao)
            {
                return Message.MESSAGE_ICAO_INVALIDO;
            }

            var db = new DBContext();

            var userExisting = db.Users.FirstOrDefault(x => x.UserName == userName);

            var testeIcao = db.Icaos.Any(x => x.UserId == userExisting.Id);
            
            if (testeIcao == true)
            {
                return Message.MESSAGE_ICAO_EXISTENTE_NA_CONTA;
            }
            else
            {
                db.Add(new Icao {
                        UserId = userExisting.Id,
                        IcaoCode = icao
                    });
                db.SaveChanges();
                    
                return $"{Message.MESSAGE_ICAO_FAVORITADO_COM_SUCESSO}";
            }
        }

        public string ReadFavoriteIcao(string userName)
        {
            var db = new DBContext();

            var userExisting = db.Users.FirstOrDefault(x => x.UserName == userName);

            var favoriteIcao = db.Icaos.FirstOrDefault(x => x.UserId == userExisting.Id);

            if (favoriteIcao != null)
            {
                return $"{favoriteIcao.IcaoCode}";
            }
            else
            {
                return null;
            }
        }

        public string UpdateFavoriteIcao(string userName, string newIcao)
        {
            var db = new DBContext();

            var userExisting = db.Users.FirstOrDefault(x => x.UserName == userName);

            var updateFavoriteIcao = db.Icaos.FirstOrDefault(x => x.UserId == userExisting.Id);

            int lengthIcao = 4;

            if (newIcao == String.Empty)
            {
                return Message.MESSAGE_CAMPO_ATUALIZAR_ICAO_VAZIO;
            }
            else if (updateFavoriteIcao == null)
            {
                return Message.MESSAGE_ICAO_VAZIO_NO_BANCO_DE_DADOS;
            }
            else if (newIcao.Length < lengthIcao || newIcao.Length > lengthIcao)
            {
                return Message.MESSAGE_ICAO_INVALIDO;
            }

            else if (newIcao == updateFavoriteIcao.IcaoCode)
            {
                return Message.MESSAGE_AERODROMO_JA_FAVORITADO;
            }
            else
            {
                updateFavoriteIcao.IcaoCode = newIcao;
                db.SaveChanges();
                return Message.MESSAGE_AERODROMO_ATUALIZADO_COM_SUCESSO;
            }
        }

        public string RemoveFavoriteIcao(string userName)
        {
            var db = new DBContext();

            var userExisting = db.Users.FirstOrDefault(x => x.UserName == userName);
            var removeFavoriteIcao = db.Icaos.FirstOrDefault(x => x.UserId == userExisting.Id);

            if (userExisting == null)
            {
                return Message.MESSAGE_USUARIO_INVALIDO;
            }
            else if (removeFavoriteIcao == null)
            {
                return Message.MESSAGE_ICAO_VAZIO_NO_BANCO_DE_DADOS;
            }
            
            db.Remove(removeFavoriteIcao.Id);
            db.SaveChanges();
            return Message.MESSAGE_AERODROMO_REMOVIDO_COM_SUCESSO;
        }
    }
}