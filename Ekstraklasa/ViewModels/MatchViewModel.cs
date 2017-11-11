using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class MatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public MatchViewModel()
        {
            Score = "Brak danych";
        }

        public MatchViewModel(MatchEntity match)
        {
            TeamA = match.Host;
            TeamB = match.Guest;
            Score = String.Format("{0}:{1}", match.ScoreHost, match.ScoreGuest);
        }
        private string _TeamA;
        public string TeamA
        {
            get
            {
                return _TeamA;
            }
            set
            {
                if(_TeamA != value)
                {
                    _TeamA = value;
                    OnPropertyChanged("TeamA");
                }
            }
        }

        private string _TeamB;
        public string TeamB
        {
            get
            {
                return _TeamB;
            }
            set
            {
                if(_TeamB != value)
                {
                    _TeamB = value;
                    OnPropertyChanged("TeamB");
                }
            }
        }

        private string _Score;
        public string Score
        {
            get
            {
                return _Score;
            }
            set
            {
                if(_Score != value)
                {
                    _Score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        private ObservableCollection<GoalControl> _GoalsA = new ObservableCollection<GoalControl>();
        public ObservableCollection<GoalControl> GoalsA
        {
            get
            {
                return _GoalsA;
            }
            set
            {
                if(_GoalsA != value)
                {
                    _GoalsA = value;
                    OnPropertyChanged("GoalsA");
                }
            }
        }

        private ObservableCollection<GoalControl> _GoalsB = new ObservableCollection<GoalControl>();
        public ObservableCollection<GoalControl> GoalsB
        {
            get
            {
                return _GoalsB;
            }
            set
            {
                if (_GoalsB != value)
                {
                    _GoalsB = value;
                    OnPropertyChanged("GoalsB");
                }
            }
        }


        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
