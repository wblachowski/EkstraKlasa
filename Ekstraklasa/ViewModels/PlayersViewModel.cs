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
    class PlayersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public PlayersViewModel()
        {
            PrepareFilters();
            UpdatePlayers();
        }

        private ICommand _clearCommand;
        public ICommand clearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param =>
                    {
                        ClearFilters();
                        UpdatePlayers();
                    });
                }
                return _clearCommand;
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

        private bool _IsProgressBarVisible = true;
        public bool IsProgressBarVisible
        {
            get
            {
                return _IsProgressBarVisible;
            }
            set
            {
                if (_IsProgressBarVisible != value)
                {
                    _IsProgressBarVisible = value;
                    OnPropertyChanged("IsProgressBarVisible");
                }
            }
        }

        private ObservableCollection<string> _Teams;
        public ObservableCollection<string> Teams
        {
            get { return _Teams; }
            set
            {
                if (_Teams != value)
                {
                    _Teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        private ObservableCollection<string> _Positions;
        public ObservableCollection<string> Positions
        {
            get { return _Positions; }
            set
            {
                if (_Positions != value)
                {
                    _Positions = value;
                    OnPropertyChanged("Positions");
                }
            }
        }

        private ObservableCollection<string> _Nationalities;
        public ObservableCollection<string> Nationalities
        {
            get { return _Nationalities; }
            set
            {
                if (_Nationalities != value)
                {
                    _Nationalities = value;
                    OnPropertyChanged("Nationalities");
                }
            }
        }

        private string _SelectedFirstname = "";
        public string SelectedFirstname
        {
            get { return _SelectedFirstname; }
            set { _SelectedFirstname = value; OnPropertyChanged("SelectedFirstname"); UpdatePlayers(); }
        }

        private string _SelectedLastname = "";
        public string SelectedLastname
        {
            get { return _SelectedLastname; }
            set { _SelectedLastname = value; OnPropertyChanged("SelectedLastname"); UpdatePlayers(); }
        }

        private string _SelectedTeam = "";
        public string SelectedTeam
        {
            get { return _SelectedTeam; }
            set { _SelectedTeam = value; OnPropertyChanged("SelectedTeam"); UpdatePlayers(); }
        }

        private string _SelectedPosition = "";
        public string SelectedPosition
        {
            get { return _SelectedPosition; }
            set { _SelectedPosition = value; OnPropertyChanged("SelectedPosition"); UpdatePlayers(); }
        }

        private string _SelectedNationality = "";
        public string SelectedNationality
        {
            get { return _SelectedNationality; }
            set { _SelectedNationality = value; OnPropertyChanged("SelectedNationality"); UpdatePlayers(); }
        }

        private int _LowerValueHeight = 150;
        public int LowerValueHeight
        {
            get { return _LowerValueHeight; }
            set
            {
                _LowerValueHeight = value;
                _UpperValueHeight = Math.Max(_UpperValueHeight, value);
                OnPropertyChanged("LowerValueHeight");
                OnPropertyChanged("LowerWidthHeight");
                OnPropertyChanged("UpperWidthHeight");
                OnPropertyChanged("UpperValueHeight");
                UpdatePlayers();
            }
        }

        private int _UpperValueHeight = 250;
        public int UpperValueHeight
        {
            get { return _UpperValueHeight; }
            set
            {
                _UpperValueHeight = value;
                _LowerValueHeight = Math.Min(_LowerValueHeight, value);
                OnPropertyChanged("UpperValueHeight");
                OnPropertyChanged("UpperWidthHeight");
                OnPropertyChanged("LowerWidthHeight");
                OnPropertyChanged("LowerValueHeight");
                UpdatePlayers();
            }
        }

        public int LowerWidthHeight
        {
            get
            {
                return (int)((double)(LowerValueHeight - MinimumHeight) / (double)(MaximumHeight - MinimumHeight) * 105.0);
            }
        }
        public int UpperWidthHeight
        {
            get
            {
                return (int)((double)(UpperValueHeight - MinimumHeight) / (double)(MaximumHeight - MinimumHeight) * 105.0) + 11;
            }
        }

        private int _MinimumHeight = 150;
        public int MinimumHeight
        {
            get { return _MinimumHeight; }
            set
            {
                _MinimumHeight = value;
                OnPropertyChanged("MinimumHeight");
            }
        }

        private int _MaximumHeight = 250;
        public int MaximumHeight
        {
            get { return _MaximumHeight; }
            set
            {
                _MaximumHeight = value;
                OnPropertyChanged("MaximumHeight");
            }
        }
        //**********************************************//
        private int _LowerValueWeight = 50;
        public int LowerValueWeight
        {
            get { return _LowerValueWeight; }
            set
            {
                _LowerValueWeight = value;
                _UpperValueWeight = Math.Max(_UpperValueWeight, value);
                OnPropertyChanged("LowerValueWeight");
                OnPropertyChanged("LowerWidthWeight");
                OnPropertyChanged("UpperWidthWeight");
                OnPropertyChanged("UpperValueWeight");
                UpdatePlayers();
            }
        }

        private int _UpperValueWeight = 150;
        public int UpperValueWeight
        {
            get { return _UpperValueWeight; }
            set
            {
                _UpperValueWeight = value;
                _LowerValueWeight = Math.Min(_LowerValueWeight, value);
                OnPropertyChanged("UpperValueWeight");
                OnPropertyChanged("UpperWidthWeight");
                OnPropertyChanged("LowerWidthWeight");
                OnPropertyChanged("LowerValueWeight");
                UpdatePlayers();
            }
        }

        public int LowerWidthWeight
        {
            get
            {
                return (int)((double)(LowerValueWeight - MinimumWeight) / (double)(MaximumWeight - MinimumWeight) * 105.0);
            }
        }
        public int UpperWidthWeight
        {
            get
            {
                return (int)((double)(UpperValueWeight - MinimumWeight) / (double)(MaximumWeight - MinimumWeight) * 105.0) + 11;
            }
        }

        private int _MinimumWeight = 50;
        public int MinimumWeight
        {
            get { return _MinimumWeight; }
            set
            {
                _MinimumWeight = value;
                OnPropertyChanged("MinimumWeight");
            }
        }

        private int _MaximumWeight = 150;
        public int MaximumWeight
        {
            get { return _MaximumWeight; }
            set
            {
                _MaximumWeight = value;
                OnPropertyChanged("MaximumWeight");
            }
        }
        //**********************************************//
        private int _LowerValueAge = 15;
        public int LowerValueAge
        {
            get { return _LowerValueAge; }
            set
            {
                _LowerValueAge = value;
                _UpperValueAge = Math.Max(_UpperValueAge, value);
                OnPropertyChanged("LowerValueAge");
                OnPropertyChanged("LowerWidthAge");
                OnPropertyChanged("UpperWidthAge");
                OnPropertyChanged("UpperValueAge");
                UpdatePlayers();
            }
        }

        private int _UpperValueAge = 45;
        public int UpperValueAge
        {
            get { return _UpperValueAge; }
            set
            {
                _UpperValueAge = value;
                _LowerValueAge = Math.Min(_LowerValueAge, value);
                OnPropertyChanged("UpperValueAge");
                OnPropertyChanged("UpperWidthAge");
                OnPropertyChanged("LowerWidthAge");
                OnPropertyChanged("LowerValueAge");
                UpdatePlayers();
            }
        }

        public int LowerWidthAge
        {
            get
            {
                return (int)((double)(LowerValueAge - MinimumAge) / (double)(MaximumAge - MinimumAge) * 105.0);
            }
        }
        public int UpperWidthAge
        {
            get
            {
                return (int)((double)(UpperValueAge - MinimumAge) / (double)(MaximumAge - MinimumAge) * 105.0) + 11;
            }
        }

        private int _MinimumAge = 15;
        public int MinimumAge
        {
            get { return _MinimumAge; }
            set
            {
                _MinimumAge = value;
                OnPropertyChanged("MinimumAge");
            }
        }

        private int _MaximumAge = 45;
        public int MaximumAge
        {
            get { return _MaximumAge; }
            set
            {
                _MaximumAge = value;
                OnPropertyChanged("MaximumAge");
            }
        }

        private async void PrepareFilters()
        {
            Teams = new ObservableCollection<string>(await Task.Run(() => MainModel.GetTeams()));

            Positions = new ObservableCollection<string>(await Task.Run(() => MainModel.GetPositions()));

            Nationalities = new ObservableCollection<string>(await Task.Run(() => MainModel.GetNationalities()));

            Tuple<int, int> MinMaxHeight = await Task.Run(() => MainModel.GetMinMaxHeight());
            Tuple<int, int> MinMaxWeight = await Task.Run(() => MainModel.GetMinMaxWeight());
            Tuple<int, int> MinMaxAge = await Task.Run(() => MainModel.GetMinMaxAge());

            MinimumHeight = MinMaxHeight.Item1;
            MaximumHeight = MinMaxHeight.Item2;
            LowerValueHeight = MinimumHeight;
            UpperValueHeight = MaximumHeight;

            MinimumWeight = MinMaxWeight.Item1;
            MaximumWeight = MinMaxWeight.Item2;
            LowerValueWeight = MinMaxWeight.Item1;
            UpperValueWeight = MinMaxWeight.Item2;

            MinimumAge = MinMaxAge.Item1;
            MaximumAge = MinMaxAge.Item2;
            LowerValueAge = MinMaxAge.Item1;
            UpperValueAge = MinMaxAge.Item2;
        }

        private void ClearFilters()
        {
            _SelectedFirstname = _SelectedLastname = _SelectedNationality = _SelectedPosition = _SelectedTeam = "";
            _LowerValueAge = _MinimumAge;
            _UpperValueAge = _MaximumAge;
            _LowerValueHeight = _MinimumHeight;
            _UpperValueHeight = _MaximumHeight;
            _LowerValueWeight = _MinimumWeight;
            _UpperValueWeight = _MaximumWeight;

            OnPropertyChanged("SelectedFirstname");
            OnPropertyChanged("SelectedLastname");
            OnPropertyChanged("SelectedNationality");
            OnPropertyChanged("SelectedPosition");
            OnPropertyChanged("SelectedTeam");

            OnPropertyChanged("LowerWidthAge");
            OnPropertyChanged("UpperWidthAge");
            OnPropertyChanged("LowerWidthHeight");
            OnPropertyChanged("UpperWidthHeight");
            OnPropertyChanged("LowerWidthWidth");
            OnPropertyChanged("UpperWidthWidth");

            OnPropertyChanged("LowerValueAge");
            OnPropertyChanged("UpperValueAge");
            OnPropertyChanged("LowerValueHeight");
            OnPropertyChanged("UpperValueHeight");
            OnPropertyChanged("LowerValueWeight");
            OnPropertyChanged("UpperValueWeight");
        }

        private async void UpdatePlayers()
        {
            List<PlayerEntity> players = await Task.Run(() => MainModel.GetPlayers(SelectedTeam, SelectedFirstname, SelectedLastname, SelectedPosition, SelectedNationality, LowerValueAge, UpperValueAge, LowerValueHeight, UpperValueHeight, LowerValueWeight, UpperValueWeight));
            Players = new ObservableCollection<PlayerEntity>(players);
            IsProgressBarVisible = false;
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
