using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ekstraklasa
{
    public delegate void delegateChangeControl(int index, UserControl control);

    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public MainViewModel()
        {
            menuSelectedIndex = 0;
        }

        private UserControl _mainControl;
        public UserControl mainControl
        {
            get
            {
                return _mainControl;
            }
            set
            {
                if(_mainControl != value)
                {
                    _mainControl = value;
                    OnPropertyChanged("mainControl");
                }
            }
        }

        private int _menuSelectedIndex = -1;
        public int menuSelectedIndex
        {
            get
            {
                return _menuSelectedIndex;
            }
            set
            {
                if(_menuSelectedIndex != value)
                {
                    _menuSelectedIndex = value;
                    ChangeMainControl(value);
                    OnPropertyChanged("menuSelectedIndex");
                }
            }
        }

        MatchesControl matchesControl;
        TableControl tableControl;
        TeamsControl teamsControl;

        TopScorersControl topScorersControl;


        private void ChangeMainControl(int index)
        {
            switch (index)
            {
                case 0: mainControl = matchesControl == null ? matchesControl = new MatchesControl(ChangeControl) : matchesControl;break;
                case 1: mainControl = tableControl == null ? tableControl = new TableControl() : tableControl;break;
                case 2: mainControl = teamsControl == null ? teamsControl = new TeamsControl(ChangeControl) : teamsControl; break;

                case 4: mainControl = topScorersControl == null? topScorersControl = new TopScorersControl() : topScorersControl;break;
            }
        }

        private void ChangeControl(int index, UserControl control)
        {
            if (control != null) {
                mainControl = control;
            }
            else
            {
                ChangeMainControl(index);
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
