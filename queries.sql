--to tables all
select name,
count(*) as games,
count(case when (team.id=team_host_id and score_host>score_guest) or (team.id=team_guest_id and score_guest>score_host) then 1 end) as wins,
count(case when score_host=score_guest then 1 end) as ties,
count(case when (team.id=team_host_id and score_host<score_guest) or (team.id=team_guest_id and score_guest<score_host) then 1 end) as loses,
sum(case when team.id=team_host_id then score_host when team.id=team_guest_id then score_guest end) as goals_scored,
sum(case when team.id=team_host_id then score_guest when team.id=team_guest_id then score_host end) as goals_conceded,
sum(case when (team.id=team_host_id and score_host>score_guest) then 3 when score_host=score_guest then 1
        when (team.id=team_guest_id and score_guest>score_host) then 3 else 0 end) as points
from team join match on team.id=team_host_id or team.id=team_guest_id group by name order by points desc;