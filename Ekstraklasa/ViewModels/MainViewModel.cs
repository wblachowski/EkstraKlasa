using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ekstraklasa
{
    public delegate void delegateChangeControl(int index, UserControl control);
    public delegate void delegateUpdateControl(int index);
    public delegate void delegateShowSnackbar(string message);

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

        private bool _IsSnabkbarActive;
        public bool IsSnackbarActive
        {
            get
            {
                return _IsSnabkbarActive;
            }
            set
            {
                if (_IsSnabkbarActive != value)
                {
                    _IsSnabkbarActive = value;
                    OnPropertyChanged("IsSnackbarActive");
                }
            }
        }

        private string _SnackbarMessage;
        public string SnackbarMessage
        {
            get
            {
                return _SnackbarMessage;
            }
            set
            {
                if (_SnackbarMessage != value)
                {
                    _SnackbarMessage = value;
                    OnPropertyChanged("SnackbarMessage");
                }
            }
        }

        MatchesControl matchesControl;
        TableControl tableControl;
        TeamsControl teamsControl;
        PlayersControl playersControl;
        TopScorersControl topScorersControl;


        private void ChangeMainControl(int index)
        {
            switch (index)
            {
                case 0: mainControl = matchesControl == null ? matchesControl = new MatchesControl(ChangeControl, UpdateControl, ShowSnackbar) : matchesControl; break;
                case 1: mainControl = tableControl == null ? tableControl = new TableControl() : tableControl; break;
                case 2: {mainControl =  teamsControl == null ? teamsControl = new TeamsControl(ChangeControl,UpdateControl, ShowSnackbar) : teamsControl;
                         var viewModel = teamsControl.DataContext as TeamsViewModel; viewModel.Initialize();
                    } break;
                case 3: mainControl = playersControl == null ? playersControl = new PlayersControl() : playersControl; break;
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
                        if (matchesControl == null) { matchesControl = new MatchesControl(ChangeControl, UpdateControl,ShowSnackbar); }
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
                case 2:
                    {
                        if (teamsControl == null) { teamsControl = new TeamsControl(ChangeControl, UpdateControl, ShowSnackbar); }
                        else
                        {
                            var viewModel = teamsControl.DataContext as TeamsViewModel; viewModel.Update();
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

        private async void ShowSnackbar(string Message)
        {
            await Task.Run(() => Thread.Sleep(500));
            SnackbarMessage = Message;
            IsSnackbarActive = true;
            await Task.Run(() => {
                Thread.Sleep(3000);
                IsSnackbarActive = false;
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
