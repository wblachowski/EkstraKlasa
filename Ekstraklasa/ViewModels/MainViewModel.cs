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
    public delegate void delegateUpdateControl(int index);

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
                if (_mainControl != value)
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
                if (_menuSelectedIndex != value)
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
                case 0: mainControl = matchesControl == null ? matchesControl = new MatchesControl(ChangeControl, UpdateControl) : matchesControl; break;
                case 1: mainControl = tableControl == null ? tableControl = new TableControl() : tableControl; break;
                case 2: mainControl = teamsControl == null ? teamsControl = new TeamsControl(ChangeControl) : teamsControl; break;

                case 4: mainControl = topScorersControl == null ? topScorersControl = new TopScorersControl() : topScorersControl; break;
            }
        }

        private void ChangeControl(int index, UserControl control)
        {
            if (control != null)
            {
                mainControl = control;
            }
            else
            {
                ChangeMainControl(index);
            }
        }

        private void UpdateControl(int index)
        {

            switch (index)
            {
                case 0:
                    {
                        if (matchesControl == null) { matchesControl = new MatchesControl(ChangeControl, UpdateControl); }
                        else
                        {
                            var viewModel = matchesControl.DataContext as MatchesViewModel; viewModel.Update();
                        }
                    }
                    break;
                case 1:
                    {
                        if (tableControl == null) { tableControl = new TableControl(); }
                        else
                        {
                            var viewModel = tableControl.DataContext as TableViewModel; viewModel.Update();
                        }
                    }
                    break;
                case 4:
                    {
                        if (topScorersControl == null) { topScorersControl = new TopScorersControl(); }
                        else
                        {
                            var viewModel = topScorersControl.DataContext as TopScorersViewModel; viewModel.Update();
                        }
                    }
                    break;
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
