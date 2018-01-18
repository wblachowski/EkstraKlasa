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
                                TableList.Add(new TableEntity(nr, name, matches, wins, ties, loses, goals, points, path));
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
                                int hostId = dr.GetInt32(3);
                                string hostPath = dr.GetString(4);
                                string guest = dr.GetString(5);
                                int guestId = dr.GetInt32(6);
                                string guestPath = dr.GetString(7);
                                int scoreHost = dr.GetInt32(8);
                                int scoreGuest = dr.GetInt32(9);
                                string stadium = dr.GetString(10);
                                string city = dr.GetString(11);
                                string address = dr.GetString(12);
                                int capacity = dr.GetInt32(13);
                                int Sid = dr.GetInt32(14);
                                Matches.Add(new MatchEntity(id, date, host, hostId, hostPath, guest, guestId, guestPath, scoreHost, scoreGuest, new StadiumEntity(Sid, stadium, address, city, capacity)));
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
                                int hostId = dr.GetInt32(3);
                                string hostPath = dr.GetString(4);
                                string guest = dr.GetString(5);
                                int guestId = dr.GetInt32(6);
                                string guestPath = dr.GetString(7);
                                int scoreHost = dr.GetInt32(8);
                                int scoreGuest = dr.GetInt32(9);
                                string stadium = dr.GetString(10);
                                string city = dr.GetString(11);
                                string address = dr.GetString(12);
                                int capacity = dr.GetInt32(13);
                                int Sid = dr.GetInt32(14);
                                Matches.Add(new MatchEntity(id, date, host, hostId, hostPath, guest, guestId, guestPath, scoreHost, scoreGuest, new StadiumEntity(Sid, stadium, address, city, capacity)));
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
                                string pesel = dr.GetInt64(1).ToString();
                                string firstname = dr.GetString(2);
                                string lastname = dr.GetString(3);
                                DateTime dateOfBirth = dr.GetDateTime(4);
                                string nationality = dr.GetString(5);
                                int weight = dr.GetInt32(6);
                                int height = dr.GetInt32(7);
                                int nr = dr.GetInt32(8);
                                string position = dr.GetString(9);
                                bool hostGoal = dr.GetInt32(10) == 1 ? true : false;
                                goals.Add(new GoalEntity(new PlayerEntity(pesel, firstname, lastname, dateOfBirth, nationality, weight, height, nr, position), minute, hostGoal));
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

        public static List<TeamEntity> GetTeams()
        {
            List<TeamEntity> teams = new List<TeamEntity>();
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
                                int id = dr.GetInt32(0);
                                string name = dr.GetString(1);
                                DateTime foundedDate = dr.GetDateTime(2);
                                string logoPath = dr.GetString(3);

                                teams.Add(new TeamEntity(id, name, logoPath, foundedDate, null, null));
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

        public static List<string> GetPositions()
        {
            List<string> positions = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetPositions;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string position = dr.GetString(0);
                                positions.Add(position);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return positions;
        }

        public static List<string> GetNationalities()
        {
            List<string> nationalities = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetNationalities;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string nation = dr.GetString(0);
                                nationalities.Add(nation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return nationalities;
        }

        public static Tuple<int, int> GetMinMaxHeight()
        {
            List<string> nationalities = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetMinMaxHeight;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int min = dr.GetInt32(0);
                                int max = dr.GetInt32(1);
                                return new Tuple<int, int>(min, max);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Tuple<int, int>(150, 250);
        }

        public static Tuple<int, int> GetMinMaxWeight()
        {
            List<string> nationalities = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetMinMaxWeight;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int min = dr.GetInt32(0);
                                int max = dr.GetInt32(1);
                                return new Tuple<int, int>(min, max);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Tuple<int, int>(50, 150);
        }

        public static Tuple<int, int> GetMinMaxAge()
        {
            List<string> nationalities = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetMinMaxAge;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int min = dr.GetInt32(0);
                                int max = dr.GetInt32(1);
                                return new Tuple<int, int>(min, max);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Tuple<int, int>(15, 45);
        }

        public static List<StadiumEntity> GetStadiums()
        {
            List<StadiumEntity> stadiums = new List<StadiumEntity>();
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
                                int id = dr.GetInt32(0);
                                string name = dr.GetString(1);
                                string city = dr.GetString(2);
                                string address = dr.GetString(3);
                                int capacity = dr.GetInt32(4);

                                stadiums.Add(new StadiumEntity(id, name, address, city, capacity));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return stadiums;
        }

        public static List<TeamEntity> GetTeamDetails(string TeamName = "")
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
                                int Id = dr.GetInt32(0);
                                string name = dr.GetString(1);
                                string path = dr.GetString(2);
                                DateTime foundedDate = dr.GetDateTime(3);
                                string stadiumName = dr.GetString(4);
                                string stadiumCity = dr.GetString(5);
                                string stadiumAddress = dr.GetString(6);
                                int stadiumCapacity = dr.GetInt32(7);
                                int stadiumId = dr.GetInt32(8);
                                long pesel = dr.GetInt64(9);
                                string firstname = dr.GetString(10);
                                string lastname = dr.GetString(11);
                                DateTime dateOfBirth = dr.GetDateTime(12);
                                string nationality = dr.GetString(13);
                                DateTime hiringDate = dr.GetDateTime(14);
                                teams.Add(new TeamEntity(Id, name, path, foundedDate, new CoachEntity(Convert.ToString(pesel), firstname, lastname, dateOfBirth, nationality, hiringDate), new StadiumEntity(stadiumId, stadiumName, stadiumAddress, stadiumCity, stadiumCapacity)));
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

        public static List<PlayerEntity> GetPlayers(string TeamName = "", string Firstname = "", string Lastname = "", string Position = "", string Nationality = "", int MinAge = -1, int MaxAge = Int32.MaxValue, int MinHeight = -1, int MaxHeight = Int32.MaxValue, int MinWeight = -1, int MaxWeight = Int32.MaxValue)
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
                        command.Parameters.Add("name", String.IsNullOrEmpty(TeamName) ? "%" : "%" + TeamName + "%");
                        command.Parameters.Add("firstname", String.IsNullOrEmpty(Firstname) ? "%" : "%" + Firstname + "%");
                        command.Parameters.Add("lastname", String.IsNullOrEmpty(Lastname) ? "%" : "%" + Lastname + "%");
                        command.Parameters.Add("position", String.IsNullOrEmpty(Position) ? "%" : "%" + Position + "%");
                        command.Parameters.Add("nationality", String.IsNullOrEmpty(Nationality) ? "%" : "%" + Nationality + "%");
                        command.Parameters.Add("minAge", MinAge);
                        command.Parameters.Add("maxAge", MaxAge);
                        command.Parameters.Add("minHeight", MinHeight);
                        command.Parameters.Add("maxHeight", MaxHeight);
                        command.Parameters.Add("minWeight", MinWeight);
                        command.Parameters.Add("maxWeight", MaxWeight);

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
                                string team = dr.GetString(11);
                                players.Add(new PlayerEntity(Convert.ToString(pesel), firstname, lastname, dateOfBirth, nationality, weight, height, nr, position, team));
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

        public static List<Tuple<PlayerEntity, int, string>> GetTopScorers()
        {
            List<Tuple<PlayerEntity, int, string>> players = new List<Tuple<PlayerEntity, int, string>>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.GetTopScorers;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int goals = dr.GetInt32(0);
                                long pesel = dr.GetInt64(1);
                                string firstname = dr.GetString(2);
                                string lastname = dr.GetString(3);
                                DateTime dateOfBirth = dr.GetDateTime(4);
                                string nationality = dr.GetString(5);
                                int weight = dr.GetInt32(6);
                                int height = dr.GetInt32(7);
                                int nr = dr.GetInt32(8);
                                string position = dr.GetString(9);
                                string team = dr.GetString(10);
                                players.Add(new Tuple<PlayerEntity, int, string>(new PlayerEntity(Convert.ToString(pesel), firstname, lastname, dateOfBirth, nationality, weight, height, nr, position), goals, team));
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

        public static int InsertMatch(MatchEntity Match)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertMatch;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("start_time", Match.Date);
                        command.Parameters.Add("score_host", Match.ScoreHost);
                        command.Parameters.Add("score_guest", Match.ScoreGuest);
                        command.Parameters.Add("stadium_id", Match.Stadium.Id);
                        command.Parameters.Add("team_host_id", Match.HostId);
                        command.Parameters.Add("team_guest_id", Match.GuestId);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertGoal(int minute, string pesel, int team_id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertGoal;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("minute", minute);
                        command.Parameters.Add("pesel", pesel);
                        command.Parameters.Add("team_id", team_id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertGoalWithId(int minute, string pesel, int team_id, int matchId)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertGoalWithId;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("minute", minute);
                        command.Parameters.Add("pesel", pesel);
                        command.Parameters.Add("team_id", team_id);
                        command.Parameters.Add("id", matchId);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertStadium(StadiumEntity stadium)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertStadium;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("name", stadium.Name);
                        command.Parameters.Add("city", stadium.City);
                        command.Parameters.Add("address", stadium.Address);
                        command.Parameters.Add("capacity", stadium.Capacity);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertTeam(TeamEntity team, int stadium_id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql;
                    //nowy stadion
                    if (stadium_id == -1)
                    {
                        sql = "insert into team(name,founded_date,logo_path,stadium_id) values(:name,:founded_date,:logo_path,(select max(id) from stadium))";
                    }
                    //juz istniejacy stadion
                    else
                    {
                        sql = "insert into team(name,founded_date,logo_path,stadium_id) values(:name,:founded_date,:logo_path,:stadium_id)";
                    }

                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("name", team.Name);
                        command.Parameters.Add("founded_date", team.FoundedDate);
                        command.Parameters.Add("logo_path", team.LogoPath);
                        if (stadium_id != -1)
                            command.Parameters.Add("stadium_id", stadium_id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;

        }

        public static int InsertPerson(PersonEntity Person)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertPerson;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("pesel", Person.Pesel);
                        command.Parameters.Add("firstname", Person.Firstname);
                        command.Parameters.Add("lastname", Person.Lastname);
                        command.Parameters.Add("date_of_birth", Person.DateOfBirth);
                        command.Parameters.Add("nationality", Person.Nationality);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertPlayer(PlayerEntity Player, int TeamID = -1)
        {
            InsertPerson(Player);
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql;
                    if (TeamID == -1)
                    {
                        sql = Queries.InsertPlayerLatestTeam;
                    }
                    else
                    {
                        sql = Queries.InsertPlayer;
                    }
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("pesel", Player.Pesel);
                        command.Parameters.Add("weight", Player.Weight);
                        command.Parameters.Add("height", Player.Height);
                        command.Parameters.Add("nr", Player.Nr);
                        if (TeamID != -1)
                        {
                            command.Parameters.Add("team_id", TeamID);
                        }
                        command.Parameters.Add("position", Player.Position);

                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int InsertCoach(CoachEntity Coach)
        {
            InsertPerson(Coach);
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.InsertCoachLatestTeam;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("pesel", Coach.Pesel);
                        command.Parameters.Add("hiring_date", Coach.DateOfHiring);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }


        public static int DeleteGoal(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.DeleteGoal;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int DeleteMatch(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.DeleteMatch;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int DeletePlayers(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.DeletePlayersFromTeam;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int DeleteCoach(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.DeleteCoachFromTeam;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int DeleteTeam(int id)
        {
            DeleteCoach(id);
            DeletePlayers(id);
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.DeleteTeam;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int UpdateMatch(MatchEntity Match)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.UpdateMatch;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("start_time", Match.Date);
                        command.Parameters.Add("score_host", Match.ScoreHost);
                        command.Parameters.Add("score_guest", Match.ScoreGuest);
                        command.Parameters.Add("stadium_id", Match.Stadium.Id);
                        command.Parameters.Add("team_host_id", Match.HostId);
                        command.Parameters.Add("team_guest_id", Match.GuestId);
                        command.Parameters.Add("id", Match.ID);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int UpdatePerson(PersonEntity Person)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.UpdatePerson;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("firstname", Person.Firstname);
                        command.Parameters.Add("lastname", Person.Lastname);
                        command.Parameters.Add("date_of_birth", Person.DateOfBirth);
                        command.Parameters.Add("nationality", Person.Nationality);
                        command.Parameters.Add("pesel", Person.Pesel);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static int UpdatePlayer(PlayerEntity Player)
        {
            UpdatePerson(Player);
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = Queries.UpdatePlayer;
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add("weight", Player.Weight);
                        command.Parameters.Add("height", Player.Height);
                        command.Parameters.Add("nr", Player.Nr);
                        command.Parameters.Add("position", Player.Position);
                        command.Parameters.Add("pesel", Player.Pesel);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static void ExecuteTransfer(string pesel, int team_id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection_string"]))
                {
                    connection.Open();
                    string sql = "transferPlayer";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("pesel", pesel);
                        command.Parameters.Add("team_id", team_id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
