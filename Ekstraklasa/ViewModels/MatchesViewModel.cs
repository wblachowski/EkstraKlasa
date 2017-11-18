using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class MatchesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public MatchesViewModel()
        {
            ObservableCollection<MatchControl> temp = new ObservableCollection<MatchControl>();
            temp.Add(new MatchControl());
            Matches = temp;
            UpdateMatches();
            UpdateFilters();
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

        private ObservableCollection<string> _HostTeams = new ObservableCollection<string>();
        public ObservableCollection<string> HostTeams
        {
            get
            {
                return _HostTeams;
            }
            set
            {
                if (_HostTeams != value)
                {
                    _HostTeams = value;
                    OnPropertyChanged("HostTeams");
                }
            }
        }

        private ObservableCollection<string> _GuestTeams = new ObservableCollection<string>();
        public ObservableCollection<string> GuestTeams
        {
            get
            {
                return _GuestTeams;
            }
            set
            {
                if (_GuestTeams != value)
                {
                    _GuestTeams = value;
                    OnPropertyChanged("GuestTeams");
                }
            }
        }

        private ObservableCollection<string> _Stadiums = new ObservableCollection<string>();
        public ObservableCollection<string> Stadiums
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

        private async void UpdateFilters()
        {
            List<string> teams = await GetCurrentFiltersAsync();
            List<string> stadiums = await GetCurrentStadiumsAsync();
            HostTeams = new ObservableCollection<string>(teams);
            GuestTeams = new ObservableCollection<string>(teams);
            Stadiums = new ObservableCollection<string>(stadiums);
        }

        private async void UpdateMatches()
        {
            List<MatchEntity> matches = await GetCurrentMatchesAsync();
            ObservableCollection<MatchControl> temp = new ObservableCollection<MatchControl>();
            foreach (MatchEntity match in matches)
            {
                temp.Add(new MatchControl(match));
            }
            Matches = temp;
        }

        private async Task<List<string>> GetCurrentStadiumsAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetStadiums();
            });
        }

        private async Task<List<string>> GetCurrentFiltersAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetTeams();
            });
        }

        private async Task<List<MatchEntity>> GetCurrentMatchesAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetCurrentMatches();
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}
