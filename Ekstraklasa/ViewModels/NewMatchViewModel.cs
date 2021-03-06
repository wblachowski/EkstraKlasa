﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ekstraklasa
{
    class NewMatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;
        public event delegateUpdateControl UpdateContentEvent = null;
        public event delegateShowSnackbar ShowSnackbarEvent = null;

        public NewMatchViewModel()
        {
            SetDateAndTime();
            UpdateTeams();
            UpdateStadiums();
        }

        public NewMatchViewModel(MatchEntity UpdatedMatch)
        {
            this.UpdatedMatch = UpdatedMatch;
            this.ScoreGuest = UpdatedMatch.ScoreGuest.ToString();
            this.ScoreHost = UpdatedMatch.ScoreHost.ToString();

            DateSelected = UpdatedMatch.Date.ToString("dd.MM.yyyy");
            TimeSelected = UpdatedMatch.Date.ToString("HH:mm");

            UpdateStadiums();
            UpdateTeams();
        }

        private MatchEntity UpdatedMatch;

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_GoBackCommand == null)
                {
                    _GoBackCommand = new RelayCommand(param =>
                    {
                        if (ChangeContentEvent != null)
                        {
                            ChangeContentEvent(0, null);
                        }
                    });
                }
                return _GoBackCommand;
            }
        }

        private ICommand _ClearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_ClearCommand == null)
                {
                    _ClearCommand = new RelayCommand(param =>
                    {
                        TeamHost = null;
                        TeamGuest = null;
                        StadiumSelected = null;
                        IsHostStadium = true;
                        ScoreHost = "";
                        ScoreGuest = "";
                        DateSelected = "";
                        TimeSelected = "";
                    });
                }
                return _ClearCommand;
            }
        }

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param =>
                    {
                        if(UpdatedMatch == null)
                        {
                            AddMatch();
                        }
                        else
                        {
                            UpdateMatch();
                        }
                    });
                }
                return _AddCommand;
            }
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
                    _ScoreHost = value.Trim();
                    OnPropertyChanged("ScoreHost");
                    OnPropertyChanged("AreThereGoals");
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
                    _ScoreGuest = value.Trim();
                    OnPropertyChanged("ScoreGuest");
                    OnPropertyChanged("AreThereGoals");
                    List<NewGuestGoalControl> goals = new List<NewGuestGoalControl>();
                    int count;
                    if (Int32.TryParse(_ScoreGuest, out count))
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

        public bool AreThereGoals
        {
            get
            {
                string sg = ScoreGuest == null ? "0" : ScoreGuest.Trim();
                string sh = ScoreHost == null ? "0" : ScoreHost.Trim();
                int sgi, shi;
                if(Int32.TryParse(sg,out sgi) && Int32.TryParse(sh, out shi))
                {
                    return sgi > 0 || shi > 0;
                }
                else
                {
                    return false;
                }
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

        private StadiumEntity _StadiumSelected;
        public StadiumEntity StadiumSelected
        {
            get
            {
                return _StadiumSelected;
            }
            set
            {
                if (_StadiumSelected != value)
                {
                    _StadiumSelected = value;
                    OnPropertyChanged("StadiumSelected");
                }
            }
        }

        private void SetDateAndTime()
        {
            DateSelected = DateTime.Now.ToString("dd.MM.yyyy");
            TimeSelected = DateTime.Now.ToString("HH:mm");
        }

        private async void AddMatch()
        {
            DateTime time = DateTime.ParseExact(DateSelected + " " + TimeSelected, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            StadiumEntity stadium = IsHostStadium ? TeamHost.Stadium : StadiumSelected;
            int addedMatch = await Task.Run(()=>MainModel.InsertMatch(new MatchEntity(0, time, TeamHost.Name, TeamHost.Id, "", TeamGuest.Name, TeamGuest.Id, "", Convert.ToInt32(ScoreHost), Convert.ToInt32(ScoreGuest), stadium)));
            int addedGoals = await AddGoals();
            if(addedMatch + addedGoals == GoalsHost.Count + GoalsGuest.Count + 1)
            {
                UpdateContent("Dodano nowy mecz");
            }
            else
            {
                UpdateContent("Błąd przy dodawaniu nowego meczu");
            }
        }

        private async void UpdateMatch()
        {
            DateTime time = DateTime.ParseExact(DateSelected + " " + TimeSelected, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            StadiumEntity stadium = IsHostStadium ? TeamHost.Stadium : StadiumSelected;
            int updatedMatch = await Task.Run(() => MainModel.UpdateMatch(new MatchEntity(UpdatedMatch.ID,time,TeamHost.Name,TeamHost.Id,"",TeamGuest.Name,TeamGuest.Id,"", Convert.ToInt32(ScoreHost), Convert.ToInt32(ScoreGuest), stadium)));
            int deletedGoals = await Task.Run(() => MainModel.DeleteGoal(UpdatedMatch.ID));
            int addedGoals = await AddGoals();
            UpdateContent("Edytowano mecz");
        }

        private async Task<int> AddGoals()
        {
            List<GoalEntity> hostGoals = new List<GoalEntity>(), guestGoals = new List<GoalEntity>();
            foreach (NewHostGoalControl control in GoalsHost)
            {
                GoalViewModel viewModel = (control.DataContext as GoalViewModel);
                hostGoals.Add(new GoalEntity(viewModel.Scorer, Convert.ToInt32(viewModel.Minute), true));
            }
            foreach (NewGuestGoalControl control in GoalsGuest)
            {
                GoalViewModel viewModel = (control.DataContext as GoalViewModel);
                guestGoals.Add(new GoalEntity(viewModel.Scorer, Convert.ToInt32(viewModel.Minute), false));
            }
            return await Task.Run(() =>
            {
                int inserted = 0;
                foreach (GoalEntity hostGoal in hostGoals)
                {
                    inserted += UpdatedMatch == null ? MainModel.InsertGoal(hostGoal.Minute, hostGoal.Scorer.Pesel, TeamHost.Id) : MainModel.InsertGoalWithId(hostGoal.Minute, hostGoal.Scorer.Pesel, TeamHost.Id,UpdatedMatch.ID);
                }

                foreach (GoalEntity guestGoal in guestGoals)
                {
                    inserted += UpdatedMatch == null ? MainModel.InsertGoal(guestGoal.Minute, guestGoal.Scorer.Pesel, TeamGuest.Id) : MainModel.InsertGoalWithId(guestGoal.Minute, guestGoal.Scorer.Pesel, TeamGuest.Id,UpdatedMatch.ID);
                }
                return inserted;
            });
        }

        private void UpdateContent(string message)
        {
            if (UpdateContentEvent != null)
            {
                UpdateContentEvent(0);
                UpdateContentEvent(1);
                UpdateContentEvent(4);
            }
            if (ChangeContentEvent != null)
            {
                ChangeContentEvent(0, null);
            }

            if (ShowSnackbarEvent != null)
            {
                ShowSnackbarEvent(message);
            }
        }

        private async void UpdateStadiums()
        {
            List<StadiumEntity> stadiums = await GetCurrentStadiumsAsync();
            Stadiums = new ObservableCollection<StadiumEntity>(stadiums);
            if (UpdatedMatch != null)
            {
                StadiumSelected = stadiums.Find(x => x.Id == UpdatedMatch.Stadium.Id);
            }
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
            if (UpdatedMatch != null)
            {
                TeamHost = teams.Find(x => x.Id == UpdatedMatch.HostId);
                if (TeamHost.Stadium.Id == UpdatedMatch.Stadium.Id)
                {
                    IsHostStadium = true;
                }
                else
                {
                    IsHostStadium = false;
                }
                TeamGuest = teams.Find(x => x.Id == UpdatedMatch.GuestId);
            }
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

            if (playersHost.Count > 0 && playersGuest.Count > 0)
            {
                playersGuest.Add(null);
            }


            playersGuest.AddRange(playersHost);

            if (playersHost.Count > 0 && playersGuestCopy.Count > 0)
            {
                playersHost.Add(null);
            }

            playersHost.AddRange(playersGuestCopy);

            PlayersHost = new ObservableCollection<PlayerEntity>(playersHost);
            PlayersGuest = new ObservableCollection<PlayerEntity>(playersGuest);
            UpdateGoals();
        }

        private async Task<List<PlayerEntity>> GetPlayersAsync(string teamName)
        {
            return await Task.Run(() =>
            {
                return MainModel.GetPlayers(teamName);
            });
        }

        private async void UpdateGoals()
        {
            if (UpdatedMatch == null)
            {
                return;
            }
            List<GoalEntity> goals = await GetGoalsAsync();
            List<NewHostGoalControl> hostGoals = new List<NewHostGoalControl>();
            List<NewGuestGoalControl> guestGoals = new List<NewGuestGoalControl>();
            foreach (GoalEntity goal in goals)
            {
                if (goal.HostGoal)
                {
                    NewHostGoalControl control = new NewHostGoalControl();
                    GoalViewModel viewModel = (control.DataContext as GoalViewModel);
                    viewModel.Minute = goal.Minute.ToString();
                    viewModel.Scorer = PlayersHost.FirstOrDefault(x => x == null ? false : x.Pesel == goal.Scorer.Pesel);
                    hostGoals.Add(control);
                }
                else
                {
                    NewGuestGoalControl control = new NewGuestGoalControl();
                    GoalViewModel viewModel = (control.DataContext as GoalViewModel);
                    viewModel.Minute = goal.Minute.ToString();
                    viewModel.Scorer = PlayersGuest.FirstOrDefault(x => x == null ? false : x.Pesel == goal.Scorer.Pesel);
                    guestGoals.Add(control);
                }
            }
            GoalsHost = new ObservableCollection<NewHostGoalControl>(hostGoals);
            GoalsGuest = new ObservableCollection<NewGuestGoalControl>(guestGoals);
        }

        private async Task<List<GoalEntity>> GetGoalsAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetGoalsByID(UpdatedMatch.ID);
            });
        }


        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
