using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
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
            UpdateStadiums();
        }

        private ICommand _OpenImageDialogCommand;
        public ICommand OpenImageDialogCommand
        {
            get
            {
                if (_OpenImageDialogCommand == null)
                {
                    _OpenImageDialogCommand = new RelayCommand(param => OpenImageDialog());
                }
                return _OpenImageDialogCommand;
            }
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_GoBackCommand == null)
                {
                    _GoBackCommand = new RelayCommand(param =>
                    {
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
                if (_AddNewPlayer == null)
                {
                    _AddNewPlayer = new RelayCommand(param => ExecutePlayerDialog(param));
                }
                return _AddNewPlayer;
            }
        }

        private string _ImagePath = ConfigurationManager.AppSettings["default_logo"];
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                if (_ImagePath != value)
                {
                    _ImagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        private ObservableCollection<StadiumEntity> _Stadiums;
        public ObservableCollection<StadiumEntity> Stadiums
        {
            get
            {
                return _Stadiums;
            }
            set
            {
                if (_Stadiums != value)
                {
                    _Stadiums = value;
                    OnPropertyChanged("Stadiums");
                }
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
                if (_Players != value)
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
                if (!_IsExistingStadium != value)
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

        private CoachEntity _DialogCoach = new CoachEntity();
        public CoachEntity DialogCoach
        {
            get
            {
                return _DialogCoach;
            }
            set
            {
                if (_DialogCoach != value)
                {
                    _DialogCoach = value;
                    OnPropertyChanged("CoachCaption");
                    OnPropertyChanged("DialogCoach");
                }
            }
        }

        public string CoachCaption
        {
            get
            {
                if (!String.IsNullOrEmpty(DialogCoach.Firstname) && !String.IsNullOrEmpty(DialogCoach.Lastname))
                {
                    return _DialogCoach.Firstname + " " + _DialogCoach.Lastname;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                CoachCaption = value;
            }
        }

        private async void UpdateStadiums()
        {
            List<StadiumEntity> stadiums = await Task.Run(() =>
            {
                return MainModel.GetStadiums();
            });
            Stadiums = new ObservableCollection<StadiumEntity>(stadiums);
        }

        private async void ExecutePlayerDialog(object o)
        {
            DialogPlayer = new PlayerEntity();
            var view = new NewPlayerDialog();
            view.DataContext = this;
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            if ((bool)result == true)
            {
                Players.Add(DialogPlayer);
            }
        }

        private bool opened = false;
        public async void ExecuteCoachDialog(object o)
        {
            if (!opened)
            {
                opened = true;
                CoachEntity CoachCopy = new CoachEntity(DialogCoach.Pesel, DialogCoach.Firstname, DialogCoach.Lastname, DialogCoach.DateOfBirth, DialogCoach.Nationality, DialogCoach.DateOfHiring);
                var view = new NewCoachDialog();
                view.DataContext = this;
                var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
                if ((bool)result == false)
                {
                    DialogCoach = CoachCopy;
                }
                OnPropertyChanged("CoachCaption");
                opened = false;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        private void OpenImageDialog()
        {
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|Image Files(*.png;*.bmp;*.jpg;*.jpeg;*.gif)|*.png;*.bmp;*.jpg;*.jpeg;*.gif";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                ImagePath = openFileDialog1.FileName;
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
