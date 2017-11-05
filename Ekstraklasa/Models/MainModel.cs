using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace Ekstraklasa
{
    public static class MainModel
    {
        #region loging

        public static int ValidateLogin(string username, string password)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql= "SELECT password FROM log_users WHERE name=:name"; 
                    using (OracleCommand command = new OracleCommand(sql,connection))
                    {
                        command.Parameters.Add("name",username);
                        OracleDataReader dr = command.ExecuteReader();
                        dr.Read();
                        if(dr.HasRows && dr.GetString(0) == GetPasswordHash(password))
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 2;
            }
        }

        public static string GetPasswordHash(string password)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        #endregion
    }
}
