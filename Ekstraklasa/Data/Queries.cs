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
            "from team left outer join match on team.id=team_host_id or team.id=team_guest_id group by name,logo_path "+
            "order by points desc, games desc, wins desc";

        public static string GetAllMatches = "select m.id, start_time,a.name as host, a.logo_path as host_path, b.name as guest, "+
            "b.logo_path as guest_path, score_host,score_guest,s.name, s.city, s.address " +
            "from match m " +
            "join team a on team_host_id = a.id " +
            "join team b on team_guest_id = b.id " +
            "join stadium s on m.stadium_id=s.id " +
            "order by start_time desc";

        public static string GetGoalsByID = "select minute, firstname, lastname, (case when g.team_id=m.team_host_id then 1 else 0 end) as HOSTGOAL "+ 
           "from match m join goal g on g.match_id=id join person p on g.player_pesel = pesel where m.id = :id";

        public static string GetTeams = "select name from team order by name desc";

        public static string GetStadiums = "select name from stadium order by name desc";
    }
}
