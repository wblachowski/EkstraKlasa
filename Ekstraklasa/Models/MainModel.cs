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
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string name = dr.GetString(0);
                                string path = dr.GetString(1);
                                int matches = dr.GetInt32(2);
                                int wins = dr.GetInt32(3);
                                int ties = dr.GetInt32(4);
                                int loses = dr.GetInt32(5);
                                string goals = dr.GetInt32(6).ToString() + ":" + dr.GetInt32(7).ToString();
                                int points = dr.GetInt32(8);
                                TableList.Add(new TableEntity(nr, name, matches, wins, ties, loses, goals, points,path));
                                nr++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return TableList;
        }

        public static List<MatchEntity> GetCurrentMatches(string host_name, string guest_name, string stadium_name, string start_date)
        {
            
            List<MatchEntity> Matches = new List<MatchEntity>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Ekstraklasa.Queries.GetMatches;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("host", String.IsNullOrEmpty(host_name) ? "%" : host_name);
                        command.Parameters.Add("guest", String.IsNullOrEmpty(guest_name) ? "%" : guest_name);
                        command.Parameters.Add("stadium", String.IsNullOrEmpty(stadium_name) ? "%" : stadium_name);
                        command.Parameters.Add("start_date", String.IsNullOrEmpty(start_date) ? "%" : start_date);
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(0);
                                DateTime date = dr.GetDateTime(1);
                                string host = dr.GetString(2);
                                string hostPath = dr.GetString(3);
                                string guest = dr.GetString(4);
                                string guestPath = dr.GetString(5);
                                int scoreHost = dr.GetInt32(6);
                                int scoreGuest = dr.GetInt32(7);
                                string stadium = dr.GetString(8);
                                string city = dr.GetString(9);
                                string address = dr.GetString(10);
                                Matches.Add(new MatchEntity(id, date, host, hostPath, guest, guestPath, scoreHost, scoreGuest, stadium, city, address));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Matches;
        }

        public static List<MatchEntity> GetCurrentMatchesTeam(string TeamName)
        { 
            List<MatchEntity> Matches = new List<MatchEntity>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Ekstraklasa.Queries.GetMatchesTeam;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("team", String.IsNullOrEmpty(TeamName) ? "%" : TeamName);
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(0);
                                DateTime date = dr.GetDateTime(1);
                                string host = dr.GetString(2);
                                string hostPath = dr.GetString(3);
                                string guest = dr.GetString(4);
                                string guestPath = dr.GetString(5);
                                int scoreHost = dr.GetInt32(6);
                                int scoreGuest = dr.GetInt32(7);
                                string stadium = dr.GetString(8);
                                string city = dr.GetString(9);
                                string address = dr.GetString(10);
                                Matches.Add(new MatchEntity(id, date, host, hostPath, guest, guestPath, scoreHost, scoreGuest, stadium, city, address));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Matches;
        }

        public static List<GoalEntity> GetGoalsByID(int ID)
        {
            List<GoalEntity> goals = new List<GoalEntity>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetGoalsByID;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", ID);
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int minute = dr.GetInt32(0);
                                string firstname = dr.GetString(1);
                                string lastname = dr.GetString(2);
                                bool hostGoal = dr.GetInt32(3) == 1 ? true : false;
                                goals.Add(new GoalEntity(minute, firstname, lastname, hostGoal));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return goals;
        }

        public static List<string> GetTeams()
        {
            List<string> teams = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetTeams;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string team = dr.GetString(0);
                                teams.Add(team);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return teams;
        }

        public static List<string> GetStadiums()
        {
            List<string> stadiums = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetStadiums;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string stadium = dr.GetString(0);
                                stadiums.Add(stadium);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return stadiums;
        }

        public static List<TeamEntity> GetTeamDetails(string TeamName ="")
        {
            List<TeamEntity> teams = new List<TeamEntity>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetTeamsDetails;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("name", String.IsNullOrEmpty(TeamName) ? "%" : TeamName);
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string name = dr.GetString(0);
                                string path = dr.GetString(1);
                                DateTime foundedDate = dr.GetDateTime(2);
                                string stadiumName = dr.GetString(3);
                                string stadiumCity = dr.GetString(4);
                                string stadiumAddress = dr.GetString(5);
                                int stadiumCapacity = dr.GetInt32(6);
                                long pesel = dr.GetInt64(7);
                                string firstname = dr.GetString(8);
                                string lastname = dr.GetString(9);
                                DateTime dateOfBirth = dr.GetDateTime(10);
                                string nationality = dr.GetString(11);
                                DateTime hiringDate = dr.GetDateTime(12);
                                teams.Add(new TeamEntity(name,path, foundedDate, new CoachEntity(Convert.ToString(pesel), firstname, lastname, dateOfBirth, nationality, hiringDate), new StadiumEntity(stadiumName, stadiumAddress, stadiumCity, stadiumCapacity)));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return teams;
        }

        public static List<PlayerEntity> GetPlayers(string TeamName = "")
        {
            List<PlayerEntity> players = new List<PlayerEntity>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetPlayers;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("name", String.IsNullOrEmpty(TeamName) ? "%" : TeamName);
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                long pesel = dr.GetInt64(0);
                                string firstname = dr.GetString(1);
                                string lastname = dr.GetString(2);
                                DateTime dateOfBirth = dr.GetDateTime(3);
                                string nationality = dr.GetString(4);
                                int weight = dr.GetInt32(5);
                                int height = dr.GetInt32(6);
                                int nr = dr.GetInt32(7);
                                string position = dr.GetString(9);
                                players.Add(new PlayerEntity(Convert.ToString(pesel), firstname, lastname, dateOfBirth, nationality, weight, height, nr, position));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return players;
        }


    }
}
