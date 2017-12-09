using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class TopScorersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public TopScorersViewModel()
        {
            UpdateScorers();
        }

        private ObservableCollection<Tuple<PlayerEntity,int,string>> _Scorers;
        public ObservableCollection<Tuple<PlayerEntity, int,string>> Scorers
        {
            get
            {
                return _Scorers;
            }
            set
            {
                if (_Scorers != value)
                {
                    _Scorers = value;
                    OnPropertyChanged("Scorers");
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

        public void Update()
        {
            UpdateScorers();
        }

        private async void UpdateScorers()
        {
            List<Tuple<PlayerEntity, int,string>> scorers = await GetScorersAsync();
            Scorers = new ObservableCollection<Tuple<PlayerEntity, int, string>>(scorers);
            IsProgressBarVisible = false;
        }

        private async Task<List<Tuple<PlayerEntity,int, string>>> GetScorersAsync()
        {
            return await Task.Run(()=>
            {
                return MainModel.GetTopScorers();
            }
            );
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
