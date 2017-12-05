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
            Minute = String.Format("{0}'", goal.Minute.ToString());
            Scorer = String.Format("{0}.{1}", goal.Scorer.Firstname.Substring(0, 1), goal.Scorer.Lastname);
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

        private string _Scorer;
        public string Scorer
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
