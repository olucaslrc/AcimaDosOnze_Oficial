using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using AcimaDosOnze_Oficial.AuthenticationSector.Data;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;

namespace AcimaDosOnze_Oficial.AuthenticationSector
{
    public class Account
    {
        private static int? IdUserLogged = null;

        public string CreateAccount(string userNameExisting, string emailUserExisting, string passwordUserExisting)
        {
            if (string.IsNullOrWhiteSpace(passwordUserExisting))
            {
                return Message.MESSAGE_CAMPO_DE_SENHA_VAZIO_LOGIN_E_REGISTRO;
            }
            else if (string.IsNullOrWhiteSpace(userNameExisting))
            {
                return Message.MESSAGE_CAMPO_DE_USUARIO_VAZIO_LOGIN_E_REGISTRO;
            }
            else if (string.IsNullOrWhiteSpace(emailUserExisting))
            {
                return Message.MESSAGE_CAMPO_DE_EMAIL_VAZIO_LOGIN_E_REGISTRO;
            }

            var db = new DBContext();

            var userExistingInDataBase = db.Users.Where(x => x.UserName == userNameExisting || x.Email == emailUserExisting).FirstOrDefault();

            if (userExistingInDataBase != null)
            {
                if (userExistingInDataBase.UserName == userNameExisting)
                {
                    return Message.MESSAGE_APELIDO_SENDO_UTILIZADO_REGISTRO;
                }
                else
                {
                    return Message.MESSAGE_EMAIL_SENDO_UTILIZADO_REGISTRO;
                }
            }

            MD5 md5HashForCreateAccount = MD5.Create();

            var hashPasswordCreateAccount = ConvertPasswordToHash(md5HashForCreateAccount, passwordUserExisting);

            db.Add(new User { 
                    UserName = userNameExisting,
                    Email = emailUserExisting,
                    Password = hashPasswordCreateAccount,
                    Role = Role.User
                });
            db.SaveChanges();

            return Message.MESSAGE_CONTA_REGISTRADA_COM_SUCESSO;
        }

        public bool LoginAccount(string userNameExisting, string passwordUserExisting)
        {
            if (string.IsNullOrWhiteSpace(passwordUserExisting) || string.IsNullOrWhiteSpace(userNameExisting))
            {
                Console.WriteLine(Message.MESSAGE_CREDENCIAIS_INVALIDAS_LOGIN);
                return false;
            }

            MD5 md5HashForAuthenticateUser = MD5.Create();

            var db = new DBContext();

            var hashPasswordLoginAccount = ConvertPasswordToHash(md5HashForAuthenticateUser, passwordUserExisting);

            var user = db.Users.Where(x => x.UserName == userNameExisting).FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine(Message.MESSAGE_USUARIO_INVALIDO_LOGIN);
                return false;
            }
            
            if (user.Password != hashPasswordLoginAccount)
            {
                Console.WriteLine(Message.MESSAGE_SENHA_INVALIDA_LOGIN);
                return false;
            }

            Account.IdUserLogged = user.Id;

            Console.WriteLine(Message.MESSAGE_LOGIN_SUCESSO);
            return true;
        }

        public bool LogoutAccount(string userName)
        {
            var db = new DBContext();

            var user = db.Users.FirstOrDefault(x => x.UserName == userName);

            var userToken = db.Tokens.FirstOrDefault(x => x.UserId == user.Id);

            if (user != null)
            {
                if (userToken.UserToken != null || userToken.UserToken != String.Empty)
                {
                    if (userToken.UserToken == userToken.UserToken)
                    {
                        db.Remove(userToken);
                        db.SaveChanges();

                        return true;
                    }
                }
            }
            return false;
        }

        public string ConvertPasswordToHash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool VerifyPassword(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = ConvertPasswordToHash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}