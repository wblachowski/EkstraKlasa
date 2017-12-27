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


        private async void PrepareFilters()
        {
            Teams = new ObservableCollection<string>(await Task.Run(() => MainModel.GetTeams()));

            Positions = new ObservableCollection<string>(await Task.Run(() => MainModel.GetPositions()));

            Nationalities = new ObservableCollection<string>(await Task.Run(() => MainModel.GetNationalities()));
        }

        private async void UpdatePlayers()
        {
            List<PlayerEntity> players = await Task.Run(()=>MainModel.GetPlayers());
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
