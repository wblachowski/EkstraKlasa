using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class MatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

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


        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
