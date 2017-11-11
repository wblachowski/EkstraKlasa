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
                if(_Matches != value)
                {
                    _Matches = value;
                    OnPropertyChanged("Matches");
                }
            }
        }

        private async void UpdateMatches()
        {
            List<MatchEntity> matches = await GetCurrentMatchesAsync();
            ObservableCollection<MatchControl> temp = new ObservableCollection<MatchControl>();
            foreach(MatchEntity match in matches)
            {
                temp.Add(new MatchControl(match));
            }
            Matches = temp;
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
