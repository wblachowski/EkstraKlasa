using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ekstraklasa
{
    class TeamDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;

        public TeamDetailsViewModel()
        {
            GetData("Wisła Kraków");
        }

        public TeamDetailsViewModel(string TeamName, delegateChangeControl ChangeControl)
        {
            GetData(TeamName);
            UpdateMatches(TeamName);
            ChangeContentEvent = ChangeControl;
        }

        private IList<DataPoint> _Points;
        public IList<DataPoint> Points
        {
            get
            {
                return _Points;
            }
            set
            {
                if (_Points != value)
                {
                    _Points = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private DateTime _FoundedDate;
        public DateTime FoundedDate
        {
            get
            {
                return _FoundedDate;
            }
            set
            {
                if (value != _FoundedDate)
                {
                    _FoundedDate = value;
                    OnPropertyChanged("FoundedDate");
                }
            }
        }

        private CoachEntity _Coach;
        public CoachEntity Coach
        {
            get
            {
                return _Coach;
            }
            set
            {
                if (_Coach != value)
                {
                    _Coach = value;
                    OnPropertyChanged("Coach");
                }
            }
        }

        private string _LogoPath;
        public string LogoPath
        {
            get
            {
                return _LogoPath;
            }
            set
            {
                if (_LogoPath != value)
                {
                    _LogoPath = value;
                    OnPropertyChanged("LogoPath");
                }
            }
        }

        private string _StadiumName;
        public string StadiumName
        {
            get
            {
                return _StadiumName;
            }
            set
            {
                if (_StadiumName != value)
                {
                    _StadiumName = value;
                    OnPropertyChanged("StadiumName");
                }
            }
        }

        private string _StadiumAddress;
        public string StadiumAddress
        {
            get
            {
                return _StadiumAddress;
            }
            set
            {
                if (_StadiumAddress != value)
                {
                    _StadiumAddress = value;
                    OnPropertyChanged("StadiumAddress");
                }
            }
        }

        private string _StadiumCapacity;
        public string StadiumCapacity
        {
            get
            {
                return _StadiumCapacity;
            }
            set
            {
                if (_StadiumCapacity != value)
                {
                    _StadiumCapacity = value;
                    OnPropertyChanged("StadiumCapacity");
                }
            }
        }

        private ObservableCollection<PlayerEntity> _Players;
        public ObservableCollection<PlayerEntity> Players
        {
            get
            {
                return _Players;
            }
            set
            {
                if (_Players != value)
                {
                    _Players = value;
                    OnPropertyChanged("Players");
                }
            }
        }

        private ObservableCollection<MatchControl> _Matches = new ObservableCollection<MatchControl>();
        public ObservableCollection<MatchControl> Matches
        {
            get
            {
                return _Matches;
            }
            set
            {
                if (_Matches != value)
                {
                    _Matches = value;
                    OnPropertyChanged("Matches");
                }
            }
        }

        private double _MinDate;
        public double MinDate
        {
            get
            {
                return _MinDate;
            }
            set
            {
                if (_MinDate != value)
                {
                    _MinDate = value;
                    OnPropertyChanged("MinDate");
                }
            }
        }
        private double _MaxDate;
        public double MaxDate
        {
            get
            {
                return _MaxDate;
            }
            set
            {
                if (_MaxDate != value)
                {
                    _MaxDate = value;
                    OnPropertyChanged("MaxDate");
                }
            }
        }


        private ICommand _GoBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_GoBackCommand == null)
                {
                    _GoBackCommand = new RelayCommand(param => this.GoBack());
                }
                return _GoBackCommand;
            }
        }

        private void GoBack()
        {
            if (ChangeContentEvent != null)
            {
                ChangeContentEvent(2,null);
            }
        }

        private async void GetData(string TeamName)
        {
            List<TeamEntity> teams = await GetTeamEntityAsync(TeamName);
            if (teams == null)
            {
                return;
            }
            TeamEntity team = teams[0];
            Name = team.Name;
            FoundedDate = team.FoundedDate;
            Coach = team.Coach;
            LogoPath = team.LogoPath;
            StadiumName = team.Stadium.Name;
            StadiumAddress = team.Stadium.Address + ", " + team.Stadium.City;
            StadiumCapacity = Convert.ToString(team.Stadium.Capacity) + " miejsc";

            List<PlayerEntity> players = await GetPlayersAsync(TeamName);
            Players = new ObservableCollection<PlayerEntity>(players);
        }

        private async void UpdateMatches(string TeamName)
        {
            List<MatchEntity> matches = await GetCurrentMatchesAsync(TeamName);
            ObservableCollection<MatchControl> temp = new ObservableCollection<MatchControl>();
            List<DataPoint> pointsTemp = new List<DataPoint>();
            //int it = matches.Count;
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            foreach (MatchEntity match in matches)
            {
                temp.Add(new MatchControl(match));
                DataPoint point = new DataPoint();
                double it = DateTimeAxis.ToDouble(match.Date);
                if (match.Date < minDate)
                {
                    minDate = match.Date;
                }
                if (match.Date > maxDate)
                {
                    maxDate = match.Date;
                }
                if (match.Host == TeamName)
                {
                    switch (Math.Sign(match.ScoreHost - match.ScoreGuest))
                    {
                        case 1: point = new DataPoint(it, 3); break;
                        case 0: point = new DataPoint(it, 1); break;
                        case -1: point = new DataPoint(it, 0); break;
                    }
                }
                else
                if (match.Guest == TeamName)
                {
                    switch (Math.Sign(match.ScoreGuest - match.ScoreHost))
                    {
                        case 1: point = new DataPoint(it, 3); break;
                        case 0: point = new DataPoint(it, 1); break;
                        case -1: point = new DataPoint(it, 0); break;
                    }
                }

                pointsTemp.Add(point);
                //it--;
            }
            Matches = temp;
            Points = pointsTemp;
            MinDate = DateTimeAxis.ToDouble(minDate.Subtract(new TimeSpan(1, 0, 0, 0)));
            MaxDate = DateTimeAxis.ToDouble(maxDate.AddDays(1));
        }

        private async Task<List<MatchEntity>> GetCurrentMatchesAsync(string TeamName)
        {
            return await Task.Run(() =>
            {
                return MainModel.GetCurrentMatchesTeam(TeamName);
            });
        }

        private async Task<List<TeamEntity>> GetTeamEntityAsync(string TeamName)
        {
            return await Task.Run(() =>
            {
                return MainModel.GetTeamDetails(TeamName);
            });
        }

        private async Task<List<PlayerEntity>> GetPlayersAsync(string TeamName)
        {
            return await Task.Run(() =>
            {
                return MainModel.GetPlayers(TeamName);
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
