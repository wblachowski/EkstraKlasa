using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public static class Queries
    {
        public static string GetTables = "select name, logo_path, " +
            "count(match.id) as games," +
            "count(case when (team.id=team_host_id and score_host>score_guest) or(team.id=team_guest_id and score_guest>score_host) then 1 end) as wins, " +
            "count(case when score_host=score_guest then 1 end) as ties, " +
            "count(case when (team.id= team_host_id and score_host<score_guest) or(team.id=team_guest_id and score_guest<score_host) then 1 end) as loses, " +
            "nvl(sum(case when team.id=team_host_id then score_host when team.id=team_guest_id then score_guest end),0) as goals_scored, " +
            "nvl(sum(case when team.id=team_host_id then score_guest when team.id=team_guest_id then score_host end),0) as goals_conceded, " +
            "sum(case when (team.id=team_host_id and score_host>score_guest) then 3 when score_host=score_guest then 1 " +
            "when(team.id=team_guest_id and score_guest>score_host) then 3 else 0 end) as points " +
            "from team left outer join match on team.id=team_host_id or team.id=team_guest_id group by name,logo_path " +
            "order by points desc, games desc, wins desc";

        public static string GetMatches = "select m.id, start_time,a.name as host, a.id, a.logo_path as host_path, b.name as guest, " +
            "b.id, b.logo_path as guest_path, score_host,score_guest,s.name, s.city, s.address, s.capacity, s.id " +
            "from match m " +
            "join team a on team_host_id = a.id " +
            "join team b on team_guest_id = b.id " +
            "join stadium s on m.stadium_id=s.id " +
            "where a.name like :host " +
            "and b.name like :guest " +
            "and s.name like :stadium " +
            "and to_char(start_time,'DD.MM.YYYY') like :start_date " +
            "order by start_time desc";

        public static string GetMatchesTeam = "select m.id, start_time,a.name as host, a.id, a.logo_path as host_path, b.name as guest, " +
            "b.id, b.logo_path as guest_path, score_host,score_guest,s.name, s.city, s.address, s.capacity, s.id " +
            "from match m " +
            "join team a on team_host_id = a.id " +
            "join team b on team_guest_id = b.id " +
            "join stadium s on m.stadium_id=s.id " +
            "where a.name like :team or b.name like :team " +
            "order by start_time desc";

        public static string GetGoalsByID = "select minute, p.pesel, firstname, lastname, date_of_birth, nationality, weight, height, nr, " +
            "position, (case when g.team_id=m.team_host_id then 1 else 0 end) as HOSTGOAL " +
            "from match m join goal g on g.match_id=id join player on player.pesel=g.player_pesel " +
            "join person p on g.player_pesel = p.pesel where m.id = :id order by minute";

        public static string GetTeams = "select name from team order by name desc";

        public static string GetStadiums = "select * from stadium order by name desc";

        public static string GetTeamsDetails = "select team.id, team.name,logo_path,founded_date,stadium.name as stadium, city, address, capacity, stadium.id, coach.pesel, " +
            "firstname,lastname,date_of_birth,nationality,hiring_date  from team join stadium on stadium_id=stadium.id " +
            "join coach on team.id=coach.team_id join person on person.pesel=coach.pesel where team.name like :name";

        public static string GetPlayers = "select * from person natural join player join team on team.id=team_id where team_id in (select id from team where name like :name) " +
            "order by team_id, (case when upper(position) like '%BRAMKARZ%' then 1 " +
            "when upper(position) like '%OBROŃCA%' then 2 " +
            "when upper(position) like '%POMOCNIK%' then 3 " +
            "when upper(position) like '%NAPASTNIK%' then 4 " +
            "end),nr";

        public static string GetTopScorers = "select goals, pesel,firstname,lastname,date_of_birth,nationality,weight,height,nr,position, name " +
            "from player natural join person " +
            "join team on team.id=team_id " +
            "natural join(select count(*) as goals, person.pesel from goal join person on player_pesel= person.pesel " +
            "join player on player_pesel = player.pesel join match on match_id = match.id " +
            "where player.team_id= goal.team_id group by person.pesel) order by goals desc";

        public static string InsertMatch = "insert into match(start_time,score_host,score_guest,stadium_id,team_host_id,team_guest_id) " +
             "values(:start_time,:score_host,:score_guest,:stadium_id,:team_host_id,:team_guest_id)";

        public static string InsertGoal = "insert into goal values(:minute,:pesel,:team_id,(select max(id) from match))";

        public static string InsertGoalWithId = "insert into goal values(:minute,:pesel,:team_id,:id)";

        public static string InsertStadium = "insert into stadium(name,city,address,capacity) values(:name,:city,:address,:capacity)";

        public static string InsertTeam = "insert into team(name,founded_date,logo_path,stadium_id) values(:name,:founded_date,:logo_path,:stadium_id)";

        public static string InsertPerson = "insert into person values(:pesel,:firstname,:lastname,:date_of_birth,:nationality)";

        public static string InsertCoach = "insert into coach values(:pesel,:hiring_date,:team_id)";

        public static string InsertCoachLatestTeam = "insert into coach values(:pesel,:hiring_date,(select max(id) from team))";

        public static string InsertPlayer = "insert into player values(:pesel,:weight,:height,:nr,:team_id,:position)";

        public static string InsertPlayerLatestTeam = "insert into player values(:pesel,:weight,:height,:nr,(select max(id) from team),:position)";

        public static string DeleteMatch = "delete from match where id=:id";

        public static string DeleteGoal = "delete from goal where match_id=:id";

        public static string DeletePlayersFromTeam = "delete from person where pesel in (select pesel from person natural join player where team_id=:id)";

        public static string DeleteCoachFromTeam = "delete from person where pesel in (select pesel from person natural join coach where team_id=:id)";

        //usuwa mecze, trenera i zawodników, zostają krotki w Person i Stadium
        public static string DeleteTeam = "delete from team where id=:id";

        public static string UpdateMatch = "update match set start_time=:start_time,score_host=:score_host,score_guest=:score_guest,stadium_id=:stadium_id, " +
            "team_host_id=:team_host_id,team_guest_id=:team_guest_id where id=:id";
    }
}
