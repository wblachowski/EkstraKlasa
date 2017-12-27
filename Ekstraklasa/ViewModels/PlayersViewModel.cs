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
