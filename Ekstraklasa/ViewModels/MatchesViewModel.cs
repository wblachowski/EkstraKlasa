using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ekstraklasa
{
    class MatchesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;
        public event delegateUpdateControl UpdateContentEvent = null;
        public event delegateShowSnackbar ShowSnackbarEvent = null;

        public MatchesViewModel()
        {
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

        private ObservableCollection<string> FullHost = new ObservableCollection<string>();
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

        private ObservableCollection<string> FullGuest = new ObservableCollection<string>();
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

        private ObservableCollection<StadiumEntity> _Stadiums = new ObservableCollection<StadiumEntity>();
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

        private string _HostSelected;
        public string HostSelected
        {
            get
            {
                return _HostSelected;
            }
            set
            {
                if (_HostSelected != value)
                {
                    _HostSelected = value;
                    GuestTeams = new ObservableCollection<string>(FullGuest);
                    GuestTeams.Remove(value);
                    UpdateMatches();
                    OnPropertyChanged("GuestTeams");
                    OnPropertyChanged("HostSelected");
                }
            }
        }

        private string _StadiumSelected;
        public string StadiumSelected
        {
            get
            {
                return _StadiumSelected;
            }
            set
            {
                _StadiumSelected = value;
                UpdateMatches();
                OnPropertyChanged("StadiumSelected");
            }
        }

        private string _GuestSelected;
        public string GuestSelected
        {
            get
            {
                return _GuestSelected;
            }
            set
            {
                if (_GuestSelected != value)
                {
                    _GuestSelected = value;
                    HostTeams = new ObservableCollection<string>(FullHost);
                    HostTeams.Remove(value);
                    UpdateMatches();
                    OnPropertyChanged("HostTeams");
                    OnPropertyChanged("GuestSelected");
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
                    UpdateMatches();
                    OnPropertyChanged("DateSelected");
                }
            }
        }

        private ICommand _clearCommand;
        public ICommand clearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param => this.ClearFilters());
                }
                return _clearCommand;
            }
        }

        private ICommand _NewMatchCommand;
        public ICommand NewMatchCommand
        {
            get
            {
                if (_NewMatchCommand == null)
                {
                    _NewMatchCommand = new RelayCommand(param => {
                        if (ChangeContentEvent != null)
                        {
                            ChangeContentEvent(0,new NewMatchControl(ChangeContentEvent, UpdateContentEvent,ShowSnackbarEvent));
                        }
                    });
                }
                return _NewMatchCommand;
            }
        }

        public void Update()
        {
            UpdateMatches();
            UpdateFilters();
            ClearFilters();
        }

        private void ClearFilters()
        {
            HostSelected = "";
            GuestSelected = "";
            StadiumSelected = "";
            DateSelected = "";
        }

        private async void UpdateFilters()
        {
            List<string> teams = await GetCurrentTeamsAsync();
            List<StadiumEntity> stadiums = await GetCurrentStadiumsAsync();
            FullHost = new ObservableCollection<string>(teams);
            FullGuest = new ObservableCollection<string>(teams);
            Stadiums = new ObservableCollection<StadiumEntity>(stadiums);
            GuestTeams = new ObservableCollection<string>(FullGuest);
            HostTeams = new ObservableCollection<string>(FullHost);
        }

        private async void UpdateMatches()
        {
            List<MatchEntity> matches = await GetCurrentMatchesAsync();
            Matches.Clear();
            foreach (MatchEntity match in matches)
            {
                Matches.Add(new MatchControl(match, UpdateContentEvent, ChangeContentEvent, ShowSnackbarEvent));
            }
        }

        private async Task<List<StadiumEntity>> GetCurrentStadiumsAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetStadiums();
            });
        }

        private async Task<List<string>> GetCurrentTeamsAsync()
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
                return MainModel.GetCurrentMatches(HostSelected,GuestSelected,StadiumSelected,DateSelected);
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}
