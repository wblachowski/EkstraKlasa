﻿using System;
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
        public static int TestConnection()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
            return 0;
        }


        #region loging

        public static int ValidateLogin(string username, string password)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = "SELECT password FROM log_users WHERE name=:name";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("name", username);
                        OracleDataReader dr = command.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows && dr.GetString(0) == GetPasswordHash(password))
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }

                }
            }
            catch (Exception ex)
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

        public static List<TableEntity> GetCurrentTable()
        {
            List<TableEntity> TableList = new List<TableEntity>();
            int nr = 1;
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Ekstraklasa.Queries.GetTables;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if(dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string name = dr.GetString(0);
                                int matches = dr.GetInt32(1);
                                int wins = dr.GetInt32(2);
                                int ties = dr.GetInt32(3);
                                int loses = dr.GetInt32(4);
                                string goals = dr.GetInt32(5).ToString() + ":" + dr.GetInt32(6).ToString();
                                int points = dr.GetInt32(7);
                                TableList.Add(new TableEntity(nr, name, matches, wins, ties, loses, goals, points));
                                nr++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return TableList;
        }
    }
}
