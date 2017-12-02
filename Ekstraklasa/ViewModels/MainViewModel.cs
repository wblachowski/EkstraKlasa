using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ekstraklasa
{
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

        private void ChangeMainControl(int index)
        {
            switch (index)
            {
                case 0: mainControl = new MatchesControl(ChangeControl);break;
                case 1: mainControl = new TableControl();break;
                case 2: mainControl = new TeamsControl(ChangeControl); break;
            }
        }

        private void ChangeControl(UserControl control)
        {
            if (control != null) {
                mainControl = control;
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
