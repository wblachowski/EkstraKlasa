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
        public event delegateShowSnackbar ShowSnackbarEvent = null;
        private TeamEntity EditedTeam;

        public NewTeamViewModel(TeamEntity EditedTeam)
        {
            BottomButtonText = "Zapisz";
            this.EditedTeam = EditedTeam;
            UpdateStadiums();
            SetTeamToEdit();
        }

        public NewTeamViewModel()
        {
            BottomButtonText = "Dodaj";
            IsExistingStadium = true;
            UpdateStadiums();
        }

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param =>
                    {
                        if (EditedTeam == null)
                        {
                            AddTeam();
                        }
                        else
                        {
                            UpdateTeam();
                        }
                    });
                }
                return _AddCommand;
            }
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

        private ICommand _DeletePlayer;
        public ICommand DeletePlayer
        {
            get
            {
                if (_DeletePlayer == null)
                {
                    _DeletePlayer = new RelayCommand(param =>
                    {
                        Players.Remove(param as PlayerEntity);
                    });
                }
                return _DeletePlayer;
            }
        }

        private ICommand _EditPlayer;
        public ICommand EditPlayer
        {
            get
            {
                if (_EditPlayer == null)
                {
                    _EditPlayer = new RelayCommand(param =>
                    {
                        PlayerEntity edited = param as PlayerEntity;
                        DialogPlayer = new PlayerEntity(edited.Pesel, edited.Firstname, edited.Lastname, edited.DateOfBirth, edited.Nationality, edited.Weight, edited.Height, edited.Nr, edited.Position);
                        ExecutePlayerDialog(param);
                    });
                }
                return _EditPlayer;
            }
        }

        private string _BottomButtonText;
        public string BottomButtonText
        {
            get { return _BottomButtonText; }
            set { _BottomButtonText = value; OnPropertyChanged("BottomButtonText"); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _Date;
        public string Date
        {
            get { return _Date; }
            set { _Date = value;Console.WriteLine(value); OnPropertyChanged("Date"); }
        }

        private StadiumEntity _StadiumSelected;
        public StadiumEntity StadiumSelected
        {
            get { return _StadiumSelected; }
            set { _StadiumSelected = value; OnPropertyChanged("StadiumSelected"); }
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

        private StadiumEntity _NewStadium = new StadiumEntity();
        public StadiumEntity NewStadium
        {
            get { return _NewStadium; }
            set { if (value != null) _NewStadium = value; }
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
            if (EditedTeam != null)
            {
                StadiumSelected = Stadiums.First(stadium => { return stadium.Id == EditedTeam.Stadium.Id; });
            }
        }

        //o jest typu PlayerEntity czyli edytujemy istniejący
        private async void ExecutePlayerDialog(object o)
        {
            if (o == null || o.GetType() != typeof(PlayerEntity))
            {
                DialogPlayer = new PlayerEntity();
            }
            var view = new NewPlayerDialog();
            view.DataContext = this;
            var result = await DialogHost.Show(view, "RootDialog");
            if ((bool)result == true && (o == null || o.GetType() != typeof(PlayerEntity)))
            {
                Players.Add(DialogPlayer);
            }
            else if ((bool)result == true && o != null && o.GetType() == typeof(PlayerEntity))
            {
                int index = Players.IndexOf(o as PlayerEntity);
                Players.RemoveAt(index);
                PlayerEntity edited = new PlayerEntity(DialogPlayer.Pesel, DialogPlayer.Firstname, DialogPlayer.Lastname, DialogPlayer.DateOfBirth, DialogPlayer.Nationality, DialogPlayer.Weight, DialogPlayer.Height, DialogPlayer.Nr, DialogPlayer.Position);
                Players.Insert(index, edited);
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
                var result = await DialogHost.Show(view, "RootDialog");
                if ((bool)result == false)
                {
                    DialogCoach = CoachCopy;
                }
                OnPropertyChanged("CoachCaption");
                opened = false;
            }
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

        private async void AddTeam()
        {
            string LogoPath = await Task.Run(() => CopyImage());
            DateTime foundedDate;
            DateTime.TryParseExact(_Date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out foundedDate);
            TeamEntity team = new TeamEntity(0, _Name, "", foundedDate, null, null);
            team.LogoPath = LogoPath;
            int stadiumIndex = -1;
            if (IsNotExistingStadium)
            {
                int insertedStadium = await Task.Run(() => MainModel.InsertStadium(NewStadium));
            }
            else
            {
                stadiumIndex = _StadiumSelected != null ? _StadiumSelected.Id : -1;
            }
            int insertedTeam = await Task.Run(() => MainModel.InsertTeam(team, stadiumIndex));
            int insertedCoach = await Task.Run(() => MainModel.InsertCoach(DialogCoach));

            foreach (PlayerEntity player in Players)
            {
                Task.Run(() => MainModel.InsertPlayer(player));
            }

            if (ChangeContentEvent != null)
            {
                ChangeContentEvent(2, null);
            }
            if (UpdateControlEvent != null)
            {
                UpdateControlEvent(1);
                UpdateControlEvent(2);
            }

            if (ShowSnackbarEvent != null)
            {
                ShowSnackbarEvent(insertedTeam == 1 ? "Dodano drużynę" : "Błąd przy dodawaniu drużyny");
            }
        }

        private async void UpdateTeam()
        {
            string LogoPath = await Task.Run(() => CopyImage());
            DateTime foundedDate;
            DateTime.TryParseExact(_Date,"dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out foundedDate);
            TeamEntity team = new TeamEntity(EditedTeam.Id, _Name, LogoPath, foundedDate, null, null);
            team.LogoPath = LogoPath;
            int stadiumIndex = -1;
            if (IsNotExistingStadium)
            {
                int insertedStadium = await Task.Run(() => MainModel.InsertStadium(NewStadium));
            }
            else
            {
                stadiumIndex = _StadiumSelected != null ? _StadiumSelected.Id : -1;
            }
            int insertedTeam = await Task.Run(() => MainModel.UpdateTeam(team, stadiumIndex));
            if (ChangeContentEvent != null)
            {
                ChangeContentEvent(2, null);
            }
            if (UpdateControlEvent != null)
            {
                UpdateControlEvent(0);
                UpdateControlEvent(1);
                UpdateControlEvent(2);
                UpdateControlEvent(3);
                UpdateControlEvent(4);
            }

            if (ShowSnackbarEvent != null)
            {
                ShowSnackbarEvent(insertedTeam == 1 ? "Edutowano drużynę" : "Błąd przy edycji drużyny");
            }
        }

        private string CopyImage()
        {
            try
            {
                if (ImagePath != ConfigurationManager.AppSettings["default_logo"])
                {
                    if (System.IO.File.Exists(ImagePath))
                    {

                        string fileName = _Name == null ? "" : _Name.Replace(' ', '_') + ".png";
                        string destFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ekstraklasa\\", fileName);
                        System.IO.File.Copy(ImagePath, destFile, true);
                        return fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        private void SetTeamToEdit()
        {
            Name = EditedTeam.Name;
            Date = EditedTeam.FoundedDate.ToString("dd.MM.yyyy");
            IsExistingStadium = true;
            DialogCoach = EditedTeam.Coach;
            ImagePath = EditedTeam.LogoPath;
            SetEditedPlayers();
        }

        private async void SetEditedPlayers()
        {
            List<PlayerEntity> players = await Task.Run(() => MainModel.GetPlayers(EditedTeam.Name));
            Players = new ObservableCollection<PlayerEntity>(players);
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
