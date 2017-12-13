using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class GoalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public GoalViewModel()
        {

        }

        public GoalViewModel(GoalEntity goal)
        {
            Minute = goal.Minute.ToString();
            Scorer = goal.Scorer;
        }

        private string _Minute;
        public string Minute
        {
            get
            {
                return _Minute;
            }
            set
            {
                if(_Minute != value)
                {
                    _Minute = value;
                    OnPropertyChanged("Minute");
                }
            }
        }

        private PlayerEntity _Scorer;
        public PlayerEntity Scorer
        {
            get
            {
                return _Scorer;
            }
            set
            {
                if(_Scorer != value)
                {
                    _Scorer = value;
                    OnPropertyChanged("Scorer");
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
