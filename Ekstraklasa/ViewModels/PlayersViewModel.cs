using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class PlayersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public PlayersViewModel()
        {
            UpdatePlayers();
            PrepareFilters();
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
            }
        }

        public int LowerWidthHeight
        {
            get
            {
                return (int)((double)(LowerValueHeight - MinimumHeight) / (double)(MaximumHeight - MinimumHeight) * 165.0);
            }
        }
        public int UpperWidthHeight
        {
            get
            {
                return (int)((double)(UpperValueHeight - MinimumHeight) / (double)(MaximumHeight - MinimumHeight) * 165.0)+11;
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
            }
        }

        public int LowerWidthWeight
        {
            get
            {
                return (int)((double)(LowerValueWeight - MinimumWeight) / (double)(MaximumWeight - MinimumWeight) * 165.0);
            }
        }
        public int UpperWidthWeight
        {
            get
            {
                return (int)((double)(UpperValueWeight - MinimumWeight) / (double)(MaximumWeight - MinimumWeight) * 165.0) + 11;
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


        private async void PrepareFilters()
        {
            Teams = new ObservableCollection<string>(await Task.Run(() => MainModel.GetTeams()));

            Positions = new ObservableCollection<string>(await Task.Run(() => MainModel.GetPositions()));

            Nationalities = new ObservableCollection<string>(await Task.Run(() => MainModel.GetNationalities()));

            Tuple<int, int> MinMaxHeight = await Task.Run(() => MainModel.GetMinMaxHeight());
            Tuple<int, int> MinMaxWeight = await Task.Run(() => MainModel.GetMinMaxWeight());

            MinimumHeight = MinMaxHeight.Item1;
            MaximumHeight = MinMaxHeight.Item2;
            LowerValueHeight = MinimumHeight;
            UpperValueHeight = MaximumHeight;

            MinimumWeight = MinMaxWeight.Item1;
            MaximumWeight = MinMaxWeight.Item2;
            LowerValueWeight = MinMaxWeight.Item1;
            UpperValueWeight = MinMaxWeight.Item2;
        }

        private async void UpdatePlayers()
        {
            List<PlayerEntity> players = await Task.Run(() => MainModel.GetPlayers());
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
