using System;
using System.Collections.Generic;
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
            ChangeContentEvent = ChangeControl;
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

        private string _Coach;
        public string Coach
        {
            get
            {
                return _Coach;
            }
            set
            {
                if(_Coach != value)
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
                ChangeContentEvent(new TeamsControl(ChangeContentEvent));
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
            Coach = team.Coach.Firstname + " " + team.Coach.Lastname;
            LogoPath = team.LogoPath;
            StadiumName = team.Stadium.Name;
            StadiumAddress = team.Stadium.Address + ", " + team.Stadium.City;
            StadiumCapacity = Convert.ToString(team.Stadium.Capacity) + " miejsc";
        }

        private async Task<List<TeamEntity>> GetTeamEntityAsync(string TeamName)
        {
            return await Task.Run(() => {
                return MainModel.GetTeamDetails(TeamName);
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
