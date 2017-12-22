using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ekstraklasa
{
    class TeamsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;
        public event delegateUpdateControl UpdateContentEvent = null;

        public TeamsViewModel()
        {
            UpdateTeams();
        }

        private ICommand _NewTeamCommand;
        public ICommand NewTeamCommand
        {
            get
            {
                if(_NewTeamCommand == null)
                {
                    _NewTeamCommand = new RelayCommand(param => {
                        if (ChangeContentEvent != null)
                    {
                        ChangeContentEvent(0, new NewTeamControl(ChangeContentEvent,UpdateContentEvent));
                    }
                    });
                }
                return _NewTeamCommand;
            }
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if(_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(DeleteTeam);
                }
                return _DeleteCommand;
            }
        }

        private ICommand _OpenDialog;
        public ICommand OpenDialog
        {
            get
            {
                if(_OpenDialog == null)
                {
                    _OpenDialog = new RelayCommand(param => ExecuteRunDialog(param)) ;
                }
                return _OpenDialog;
            }
        }

        private ObservableCollection<TeamEntity> _Teams = new ObservableCollection<TeamEntity>();
        public ObservableCollection<TeamEntity> Teams
        {
            get
            {
                return _Teams;
            }
            set
            {
                if (_Teams != value)
                {
                    _Teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        private TeamEntity _SelectedItem;
        public TeamEntity SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    if (_SelectedItem!= null && ChangeContentEvent != null)
                    {
                        ChangeContentEvent(0,new TeamDetailsControl(SelectedItem.Name, ChangeContentEvent));
                    }
                }
            }
        }

        public void Initialize()
        {
            SelectedItem = null;
        }

        private async void UpdateTeams()
        {
            List<TeamEntity> temp = await GetTeamsAsync();
            Teams = new ObservableCollection<TeamEntity>(temp);
        }

        private async Task<List<TeamEntity>> GetTeamsAsync()
        {
            return await Task.Run(()=> {
                return MainModel.GetTeamDetails();
            });
        }

        private async void ExecuteRunDialog(object o)
        {
            var view = new SimpleYesNoDialog("Czy na pewno chcesz usunąć drużynę " + (o as TeamEntity).Name + "?");
            var result = await DialogHost.Show(view, "RootDialog");
            if((bool)result == true)
            {
                Task.Run(() => MainModel.DeleteTeam((o as TeamEntity).Id));
            }
        }

        private void DeleteTeam(object obj)
        {
           
        }

        public void Update() {
            UpdateTeams();
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
