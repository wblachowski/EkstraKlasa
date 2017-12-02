using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class NewMatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public NewMatchViewModel()
        {
            SetDateAndTime();
            UpdateTeams();
            UpdateStadiums();
        }

        private string _ScoreHost;
        public string ScoreHost
        {
            get
            {
                return _ScoreHost;
            }
            set
            {
                if (_ScoreHost != value)
                {
                    _ScoreHost = value;
                    OnPropertyChanged("ScoreHost");
                    List<NewHostGoalControl> goals = new List<NewHostGoalControl>();
                    int count;
                    if (Int32.TryParse(_ScoreHost, out count))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            goals.Add(new NewHostGoalControl());
                        }
                    }
                    GoalsHost = new ObservableCollection<NewHostGoalControl>(goals);
                }

            }

        }

        private string _ScoreGuest;
        public string ScoreGuest
        {
            get
            {
                return _ScoreGuest;
            }
            set
            {
                if (_ScoreGuest != value)
                {
                    _ScoreGuest = value;
                    OnPropertyChanged("ScoreGuest");
                    List<NewGuestGoalControl> goals = new List<NewGuestGoalControl>();
                    int count;
                    if (Int32.TryParse(_ScoreGuest,out count))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            goals.Add(new NewGuestGoalControl());
                        }
                    }
                    GoalsGuest = new ObservableCollection<NewGuestGoalControl>(goals);
                }
            }
        }

        private ObservableCollection<NewHostGoalControl> _GoalsHost;
        public ObservableCollection<NewHostGoalControl> GoalsHost
        {
            get
            {
                return _GoalsHost;
            }
            set
            {
                if (_GoalsHost != value)
                {
                    _GoalsHost = value;
                    OnPropertyChanged("GoalsHost");
                }
            }
        }

        private ObservableCollection<StadiumEntity> _Stadiums;
        public ObservableCollection<StadiumEntity> Stadiums
        {
            get
            {
                return _Stadiums;
            }
            set
            {
                if (_Stadiums != value)
                {
                    _Stadiums = value;
                    OnPropertyChanged("Stadiums");
                }
            }
        }

        private ObservableCollection<NewGuestGoalControl> _GoalsGuest;
        public ObservableCollection<NewGuestGoalControl> GoalsGuest
        {
            get
            {
                return _GoalsGuest;
            }
            set
            {
                if (_GoalsGuest != value)
                {
                    _GoalsGuest = value;
                    OnPropertyChanged("GoalsGuest");
                }
            }
        }

        private ObservableCollection<TeamEntity> _Teams;
        public ObservableCollection<TeamEntity> Teams
        {
            get
            {
                return _Teams;
            }
            set
            {
                if (_Teams != value)
                {
                    _Teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        private ObservableCollection<PlayerEntity> _PlayersHost = new ObservableCollection<PlayerEntity>();
        public ObservableCollection<PlayerEntity> PlayersHost
        {
            get
            {
                return _PlayersHost;
            }
            set
            {
                if (_PlayersHost != value)
                {
                    _PlayersHost = value;
                    OnPropertyChanged("PlayersHost");
                }
            }
        }

        private ObservableCollection<PlayerEntity> _PlayersGuest = new ObservableCollection<PlayerEntity>();
        public ObservableCollection<PlayerEntity> PlayersGuest
        {
            get
            {
                return _PlayersGuest;
            }
            set
            {
                if (_PlayersGuest != value)
                {
                    _PlayersGuest = value;
                    OnPropertyChanged("PlayersGuest");
                }
            }
        }

        private TeamEntity _TeamHost;
        public TeamEntity TeamHost
        {
            get
            {
                return _TeamHost;
            }
            set
            {
                if (_TeamHost != value)
                {
                    _TeamHost = value;
                    OnPropertyChanged("TeamHost");
                    UpdatePlayers();
                }
            }
        }

        private TeamEntity _TeamGuest;
        public TeamEntity TeamGuest
        {
            get
            {
                return _TeamGuest;
            }
            set
            {
                if (_TeamGuest != value)
                {
                    _TeamGuest = value;
                    OnPropertyChanged("TeamGuest");
                    UpdatePlayers();
                }
            }
        }

        private bool _IsHostStadium = true;
        public bool IsHostStadium
        {
            get
            {
                return _IsHostStadium;
            }
            set
            {
                if (_IsHostStadium != value)
                {
                    _IsHostStadium = value;
                    OnPropertyChanged("IsHostStadium");
                    OnPropertyChanged("IsHostStadiumBox");
                }
            }
        }
        
        public bool IsHostStadiumBox
        {
            get
            {
                return !_IsHostStadium;
            }
        }

        private string _DateSelected = "";
        public string DateSelected
        {
            get
            {
                return _DateSelected;
            }
            set
            {
                if (_DateSelected != value)
                {
                    _DateSelected = value;
                    OnPropertyChanged("DateSelected");
                }
            }
        }

        private string _TimeSelected = "";
        public string TimeSelected
        {
            get
            {
                return _TimeSelected;
            }
            set
            {
                if (_TimeSelected != value)
                {
                    _TimeSelected = value;
                    OnPropertyChanged("TimeSelected");
                }
            }
        }

        private void SetDateAndTime()
        {
            DateSelected = DateTime.Now.ToString("dd.MM.yyyy");
            TimeSelected = DateTime.Now.ToString("HH:mm");
        }

        private async void UpdateHostGoals()
        {
            await Task.Run(()=> {
                List<NewHostGoalControl> goals = new List<NewHostGoalControl>();
                int count;
                if (Int32.TryParse(_ScoreHost, out count))
                {
                    for (int i = 0; i < count; i++)
                    {
                        goals.Add(new NewHostGoalControl());
                    }
                }
                GoalsHost = new ObservableCollection<NewHostGoalControl>(goals);
            });
        }


        private List<NewHostGoalControl> GetHostGoals()
        {
            List<NewHostGoalControl> goals = new List<NewHostGoalControl>();
            int count;
            if (Int32.TryParse(_ScoreHost, out count))
            {
                for (int i = 0; i < count; i++)
                {
                    goals.Add(new NewHostGoalControl());
                }
            }
            return goals;
        }

        private async void UpdateStadiums()
        {
            Stadiums = new ObservableCollection<StadiumEntity>(await GetCurrentStadiumsAsync());
        }

        private async Task<List<StadiumEntity>> GetCurrentStadiumsAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetStadiums();
            });
        }

        private async void UpdateTeams()
        {
            List<TeamEntity> teams = await GetTeamsAsync();
            Teams = new ObservableCollection<TeamEntity>(teams);
        }

        private async Task<List<TeamEntity>> GetTeamsAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetTeamDetails();
            });
        }


        private async void UpdatePlayers()
        {
            List<PlayerEntity> playersHost = new List<PlayerEntity>();
            List<PlayerEntity> playersGuest = new List<PlayerEntity>();
            if (TeamHost != null)
            {
                playersHost = await GetPlayersAsync(TeamHost.Name);
            }
            if (TeamGuest != null)
            {
                playersGuest = await GetPlayersAsync(TeamGuest.Name);
            }
            List<PlayerEntity> playersGuestCopy = new List<PlayerEntity>(playersGuest);

            if (TeamHost != null && TeamGuest != null)
            {
                playersGuest.Add(null);
            }
            playersGuest.AddRange(playersHost);
            if (TeamHost != null && TeamGuest != null)
            {
                playersHost.Add(null);
            }
            playersHost.AddRange(playersGuestCopy);

            PlayersHost = new ObservableCollection<PlayerEntity>(playersHost);
            PlayersGuest = new ObservableCollection<PlayerEntity>(playersGuest);
        }

        private async Task<List<PlayerEntity>> GetPlayersAsync(string teamName)
        {
            return await Task.Run(() =>
            {
                return MainModel.GetPlayers(teamName);
            });
        }


        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
