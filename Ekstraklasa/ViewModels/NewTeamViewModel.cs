using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ekstraklasa
{
    public class NewTeamViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;
        public event delegateUpdateControl UpdateControlEvent = null;

        public NewTeamViewModel()
        {
            IsExistingStadium = true;
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if(_GoBackCommand == null)
                {
                    _GoBackCommand = new RelayCommand(param => {
                        if (ChangeContentEvent != null)
                        {
                            ChangeContentEvent(2, null);
                        }
                    });
                }
                return _GoBackCommand;
            }
        }

        private ICommand _AddNewPlayer;
        public ICommand AddNewPlayer
        {
            get
            {
                if(_AddNewPlayer == null)
                {
                    _AddNewPlayer = new RelayCommand(param => ExecuteRunDialog(param));
                }
                return _AddNewPlayer;
            }
        }

        private ObservableCollection<PlayerEntity> _Players = new ObservableCollection<PlayerEntity>();
        public ObservableCollection<PlayerEntity> Players
        {
            get
            {
                return _Players;
            }
            set
            {
                if(_Players != value)
                {
                    _Players = value;
                    OnPropertyChanged("Players");
                }
            }
        }

        private bool _IsExistingStadium = true;
        public bool IsExistingStadium
        {
            get
            {
                return _IsExistingStadium;
            }
            set
            {
                if (_IsExistingStadium != value)
                {
                    _IsExistingStadium = value;
                    OnPropertyChanged("IsExistingStadium");
                    OnPropertyChanged("IsNotExistingStadium");
                }
            }
        }

        public bool IsNotExistingStadium
        {
            get
            {
                return !_IsExistingStadium;
            }
            set
            {
                if(!_IsExistingStadium != value)
                {
                    _IsExistingStadium = !value;
                    OnPropertyChanged("IsNotExistingStadium");
                    OnPropertyChanged("IsExistingStadium");
                }
            }
        }

        private PlayerEntity _DialogPlayer = new PlayerEntity();
        public PlayerEntity DialogPlayer
        {
            get
            {
                return _DialogPlayer;
            }
            set
            {
                if (_DialogPlayer != value)
                {
                    _DialogPlayer = value;
                    OnPropertyChanged("DialogPlayer");
                }
            }
        }

        private async void ExecuteRunDialog(object o)
        {
            DialogPlayer = new PlayerEntity();
            var view = new NewPlayerDialog();
            view.DataContext = this;
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            if((bool)result == true)
            {
                Players.Add(DialogPlayer);
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
